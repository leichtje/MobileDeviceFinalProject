using SQLite;

namespace MobileDeviceFinalProject
{
    // table to store medication info
    
    [Table("medications_table")]
    public class Medication
    {
        [PrimaryKey, AutoIncrement]
        [Column("entry_id")]
        public int EntryId { get; set; }
        
        [Column("medication_name")]
        public string? MedName { get; set; }
        
        [Column("med_dosage")]
        public string? Dosage { get; set; }
        
        [Column("special_instructions")]
        public string? MedInstructions { get; set; }
        
        [Column("time_taken")]
        public string? TimeTaken { get; set; }
        
        [Column("days_taken")]
        public string? DaysTaken { get; set; }
        
    }
}