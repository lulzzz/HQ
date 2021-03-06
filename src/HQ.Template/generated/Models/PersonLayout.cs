/*
	This code was generated by a tool. (c) 2019 HQ.IO Corporation. All rights reserved.
    Generated For: HQ.io
    Generated On: Sunday, March 10, 2019 5:13:38 AM
*/

using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using HQ.Data.Contracts;
using HQ.Data.Streaming.Fields;
using HQ.Data.Streaming;


namespace HQ.Template
{
    public ref struct PersonLayout
    {
        public StringField Name;
        public StringField Welcome;
        public StringField ExtraFields;

        public long FieldCount => 2;
        public int Fields { get; private set; }
        public long LineNumber { get; }

        public PersonLayout(LineConstructor constructor, Encoding encoding, byte[] separator)
        {
            Name = default;
            Welcome = default;
            ExtraFields = default;
            Fields = 0;
            LineNumber = constructor.lineNumber;

            SetFromLineConstructor(constructor, encoding, separator);
        }

        public unsafe void SetFromLineConstructor(LineConstructor constructor, Encoding encoding, byte[] separator)
        {
            fixed (byte* from = constructor.buffer)
            {
                var start = from;
                var length = constructor.length;
                var fields = 0;
                while(true)
                {
                    var line = new ReadOnlySpan<byte>(start, length);
                    var next = line.IndexOf(separator);
                    if(next == -1)
                    {
                        if(line.IndexOf((byte) '\r') > -1)
                            length -= 2;
                        else if(line.IndexOf((byte) '\n') > -1)
                            length -= 1;
                        next = length;
                    }

                    var consumed = next + separator.Length;
                    length -= next + separator.Length;

                    switch(fields)
                    {
                        case 0:
                            Name = new StringField(start, next, encoding);
                            break;
                        case 1:
                            Welcome = new StringField(start, next, encoding);
                            break;
                            default:
                                ExtraFields = ExtraFields.Initialized ? ExtraFields.AddLength(next) : new StringField(start, next, encoding);
                                break;
                    }

                    start += consumed;
                    fields++;
                    if(length < 1)
                    {
                        Fields = fields;
                        break;
                    }
                }
            }
        }
    }
}
