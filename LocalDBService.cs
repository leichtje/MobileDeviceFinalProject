using SQLite;

namespace MobileDeviceFinalProject {
    
    // DB functions
    
    public class LocalDbService {
        
        private const string DB_NAME = "PillZilla";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService() {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Medication>();
        }

        public async Task<List<Medication>> GetMedications() {
            return await _connection.Table<Medication>().ToListAsync();
        }
        
        public async Task<Medication> GetByEntryId(int id) {
            return await _connection.Table<Medication>().Where(x => x.EntryId == id).FirstOrDefaultAsync();
        }

        public async Task Create(Medication medication) {
            await _connection.InsertAsync(medication);
        }

        public async Task Update(Medication medication) {
            await _connection.UpdateAsync(medication);
        }

        public async Task Delete(Medication medication) {
            await _connection.DeleteAsync(medication);
        }

        // drop all rows in the DB. for testing 
        public async Task DropAllRows() {
           await _connection.ExecuteAsync("DELETE FROM medications_table");
        }
    }
}