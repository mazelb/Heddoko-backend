/**
 * @file Channel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/

namespace DAL.Models
{
   public class Channel
    {
        public string Name { get; set; }

        public User User { get; set; }
    }
}
