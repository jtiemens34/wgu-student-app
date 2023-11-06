using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev
{
    public class DatabaseHandler
    {
        private SQLiteAsyncConnection _db;

        public string StatusMessage { get; set; }
        public DatabaseHandler()
        {
        }
        async Task Init()
        {
            // DB has already been initialized, return.
            if (_db != null) return;

            string DbFileName = "db.db";
            SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
            string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DbFileName);
#if WINDOWS || IOS
            DbPath = Path.Combine(FileSystem.AppDataDirectory, DbFileName);
#endif
            _db = new SQLiteAsyncConnection(DbPath, Flags);
            PermissionStatus readStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
            PermissionStatus writeStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
            try
            {
                await _db.CreateTableAsync<Course>();
                await _db.CreateTableAsync<Term>();
                await _db.CreateTableAsync<Assessment>();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }

        #region Terms
        public async Task<List<Term>> GetAllTermsAsync()
        {
            try
            {
                await Init();
                return await _db.Table<Term>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            return new List<Term>();
        }
        public async Task<int> SaveTermAsync(Term term)
        {
            await Init();
            if (term.Id != 0) return await _db.UpdateAsync(term);
            else return await _db.InsertAsync(term);
        }
        public async Task<Term> GetTermAsync(int id)
        {
            await Init();
            return await _db.Table<Term>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> DeleteTermAsync(Term term)
        {
            await Init();
            return await _db.DeleteAsync(term);
        }
        #endregion

        #region Courses
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            try
            {
                await Init();
                return await _db.Table<Course>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            return new List<Course>();
        }
        public async Task<List<Course>> GetAllCoursesFromTermAsync(Term term)
        {
            List<Course> courses = new();
            int termId = term.Id - 1;
            try
            {
                await Init();
                courses = await _db.Table<Course>().Where(o => o.TermId == termId).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            return courses;
        }
        public async Task<int> SaveCourseAsync(Course course) 
        {
            await Init();
            if (course.Id != 0) return await _db.UpdateAsync(course);
            else return await _db.InsertAsync(course);
        }
        public async Task<Course> GetCourseAsync(int id) 
        {
            await Init();
            return await _db.Table<Course>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> DeleteCourseAsync(Course course) 
        {
            await Init();
            return await _db.DeleteAsync(course);
        }
        #endregion

        #region Assessments
        public async Task<List<Assessment>> GetAllAssessmentsAsync()
        {
            try
            {
                await Init();
                return await _db.Table<Assessment>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            return new List<Assessment>();
        }
        public async Task<int> SaveAssessmentAsync(Assessment assessment)
        {
            await Init();
            if (assessment.Id != 0) return await _db.UpdateAsync(assessment);
            else return await _db.InsertAsync(assessment);
        }
        public async Task<Assessment> GetAssessmentAsync(int id)
        {
            await Init();
            return await _db.Table<Assessment>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> DeleteAssessmentAsync(Assessment assessment)
        {
            await Init();
            return await _db.DeleteAsync(assessment);
        }
        #endregion
    }
}
