/**
 * @file IssueType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
