using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AGWorld_Listings_App
{
    internal class Note : IComparable<Note>
    {
        public static String defaultAuthor = "some Author", defaultContact = "no contact";

        [JsonInclude]
        public String _author;
        [JsonInclude]
        public String _contact;
        [JsonInclude]
        public String _content;

        [JsonConstructor]
        public Note(String _author, String _contact, String _content)
        {
            this._author = _author;
            this._contact = _contact;
            this._content = _content;
        }

        public Note(String content)
        {
            _author = defaultAuthor;
            _contact = defaultContact;
            this._content = content;
        }

        public Note(Note n)
        {
            _author = n._author;
            _contact = n._contact;
            this._content = n._content;
        }

        public void setContent(String c) { _content = c; }

        public static void setDefaultContact(String Author, String contact)
        {
            defaultAuthor = Author;
            defaultContact = contact;
        }

        public String getContent() { return _content; }
        public String getAuthor() { return _author; }
        public String getContact() { return _contact; }

        public static bool operator ==(Note left, Note right)
        {
            return left._author == right._author;
        }
        public static bool operator !=(Note left, Note right)
        {
            return left._author != right._author;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Note)
            {
                return (Note)obj == this;
            }
            if (obj is String)
            {
                return (String)obj == this._author;
            }
            return false;
        }

        public int CompareTo(Note? other)
        {
            return _author.CompareTo(other?._author);
        }
    }
}
