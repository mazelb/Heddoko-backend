namespace DAL.Models
{
    public enum IssueType
    {
        [StringValue("[FEATURES]")]
        NewFeature,
        [StringValue("[HARDWARE BUGS]")]
        Hardware,
        [StringValue("[SOFTWARE BUGS]")]
        Software
    }
}
