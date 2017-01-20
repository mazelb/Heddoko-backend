/**
 * @file ErrorAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace Heddoko.Models
{
    public class ErrorAPIModel
    {
        public ErrorAPIType Type { get; set; }

        public string Key { get; set; }

        public string Message { get; set; }
    }
}