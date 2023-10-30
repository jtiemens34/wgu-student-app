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
        public DatabaseHandler()
        {
        }
        async Task Init()
        {
            // DB has already been initialized, return.
            if (_db != null) return;

            string DbFileName = "db.db3";
            SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
            string DbPath = Path.Combine(FileSystem.AppDataDirectory, DbFileName);
            _db = new SQLiteAsyncConnection(DbPath, Flags);
            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Assessment>();
        }
        public async Task AddNewTerm(string name)
        {
            int result = 0;
            try
            {
                await Init();
                if (string.IsNullOrEmpty(name)) throw new Exception("Valid name required!");

                Term term = new Term();
                term.Id = 1;
                term.Name = name;
                term.StartDate = DateTime.Now;
                term.EndDate = DateTime.Now;
                result = await _db.InsertAsync(term);
            } 
            catch (Exception ex)
            {
                // TODO: Handle exceptions
            }
        }
        public async Task<List<Term>> GetAllTerms()
        {
            try
            {
                await Init();
                return await _db.Table<Term>().ToListAsync();
            }
            catch (Exception ex)
            {
                // TODO: Handle exceptions
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
    }
}
