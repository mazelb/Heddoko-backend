﻿/**
 * @file StreamMessage.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/

namespace Heddoko.Models.Streaming
{
    public class StreamMessage
    {
        public StreamMessageType MessageType { get; set; }

        public byte[] Message { get; set; }
    }
}