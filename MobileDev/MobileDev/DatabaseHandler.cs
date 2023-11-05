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
                await _db.CreateTableAsync<Note>();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }

        #region Terms
        public async Task AddNewTermAsync(string name)
        {
            try
            {
                await Init();
                if (string.IsNullOrEmpty(name)) throw new Exception("Valid name required!");

                Term term = new()
                {
                    Id = 1,
                    Name = name,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                };
                await _db.InsertAsync(term);
            } 
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }
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
        public async Task AddNewCourseAsync(string name) 
        {
            try
            {
                await Init();
                if (string.IsNullOrEmpty(name)) throw new Exception("Valid name required!");

                Course course = new()
                {
                    Id = 1,
                    Name = name,
                    CourseStatus = CourseStatus.InProgress,
                    TermId = 1,
                    Credits = 3
                };
                await _db.InsertAsync(course);
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
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

        #region Notes
        public async Task AddNewNoteAsync(int courseId)
        {
            try
            {
                await Init();
                if (courseId == 0) throw new Exception("Valid course ID required!");

                Note note = new()
                {
                    CourseId = courseId
                };
                await _db.InsertAsync(note);
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }
        public async Task<List<Note>> GetAllNotesFromCourseAsync(int courseId)
        {
            List<Note> notes = new();
            courseId--;
            try
            {
                await Init();
                notes = await _db.Table<Note>().Where(o => o.CourseId == courseId).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            return notes;
        }
        public async Task<int> SaveNoteAsync(Note note)
        {
            await Init();
            if (note.Id != 0) return await _db.UpdateAsync(note);
            else return await _db.InsertAsync(note);
        }
        public async Task<Note> GetNoteAsync(int id)
        {
            await Init();
            return await _db.Table<Note>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> DeleteNoteAsync(Note note)
        {
            await Init();
            return await _db.DeleteAsync(note);
        }
        #endregion

        #region Assessments
        public async Task AddNewAssessmentAsync(string name)
        {
            try
            {
                await Init();
                if (string.IsNullOrEmpty(name)) throw new Exception("Valid name required!");

                Assessment assessment = new()
                {
                    Id = 1,
                    Name = name,
                    CourseId = 1,
                    Date = DateTime.Now,
                    Type = AssessmentType.Performance
                };
                await _db.InsertAsync(assessment);
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }
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
