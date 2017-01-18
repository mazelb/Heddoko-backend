/**
 * @file HistoryNotes.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;

namespace DAL.Models
{
    public class HistoryNotes
    {
        public User User { get; set; }

        public string Notes { get; set; }

        public DateTime Created { get; set; }
    }
}