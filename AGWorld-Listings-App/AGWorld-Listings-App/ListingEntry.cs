using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AGWorld_Listings_App
{
    internal class ListingEntry
    {
        public enum Priority { Disregarded, Low, High, PastDate }
        [JsonInclude]
        private Listing_Info _listing;
        [JsonInclude]
        private Priority _priority;
        [JsonInclude]
        private List<Note> _notes;

        public ListingEntry(Listing_Info Listing)
        {
            _listing = Listing;
            _priority = Listing.getAuctionDate() < DateTime.Today ? Priority.PastDate : Priority.Low;
            _notes = new List<Note>();
            _notes.Add(new Note(""));
        }

        [JsonConstructor]
        public ListingEntry(Listing_Info _listing, Priority _priority, List<Note> _notes) : this(_listing)
        {
            this._priority = _listing.getAuctionDate() < DateTime.Today ? Priority.PastDate : _priority;
            this._notes = _notes;
            if (_notes.Count == 0) _notes.Add(new Note("Blank note"));
        }

        public ListingEntry(ListingEntry other)
        {
            _listing = new Listing_Info(other._listing);
            _priority = other._priority;
            _notes = new List<Note>();
            foreach(Note note in other._notes) { _notes.Add(new Note(note)); }
        }

        public void setAll(Listing_Info _listing, int _priority, List<Note> _notes)
        {
            this._listing = _listing;
            this._priority = (Priority)_priority;
            this._notes = new List<Note>();
            foreach(Note n in _notes)
            {
                this._notes.Add(n);
            }
        }

        public int getPriority() { return (int)_priority; }
        public String getAddress() { return _listing.getName(); }

        public List<Note> getNotes() { return _notes; }

        public Listing_Info getListing() { return _listing; }

        public Note getNoteByAuthor(String author)
        {
            foreach (Note note in _notes)
            {
                if (note._author.Equals(author)) return note;
            }
            return _notes[0];
        }

        public String[] toDataGrideRow()
        {
            String[] listingArr = _listing.toDataGridRow();
            listingArr[listingArr.Length-1] = _notes[0].getContent();
            String[] returnArr = new string[listingArr.Length + 1];
            returnArr[0] = _priority.ToString();
            for (int i = 0; i < listingArr.Length; i++)
            {
                returnArr[i + 1] = listingArr[i];
            }
            return returnArr;
        }

        public void fromDataGridRow(DataGridViewRow dvr)
        {
            _priority = Enum.Parse<Priority>(dvr.Cells[0].Value.ToString());

        }


        //combines 
        public void updateNotes(ListingEntry Other)
        {
            this._notes = combineNotes(this, Other);
        }

        public void editNote(String content, String thisAuthor, String thisContact)
        {
            if (!(_notes[0].Equals(thisAuthor)))
            {
                _notes.Insert(0, new Note(content));
            }
            else
            {
                _notes[0].setContent(content);
            }
        }

        public void delNote(Note selectedNote)
        {
            int index = _notes.IndexOf(selectedNote);
            _notes.Remove(selectedNote);
            if(_notes.Count == 0)
            {
                _notes.Add(new Note(""));
            }
        }
        private static void addNoteTool(List<Note> _notes, Note n)
        {
            if (_notes.Contains(n))
            {
                n._author += "_";
                _notes.Add(n);
            }
            else
            {
                Note mainNote = _notes[0];
                _notes.Remove(mainNote);
                _notes.Add(n);
                _notes.Sort();
                _notes.Insert(0, mainNote);
            }
        }

        public static List<Note> combineNotes(ListingEntry a, ListingEntry b)
        {
            List<Note> _notes = a._notes;
            List<Note> OtherNotes = b._notes;
            foreach (Note Note in OtherNotes)
            {
                addNoteTool(_notes, Note);
            }
            return _notes;
        }

        public static ListingEntry operator +(ListingEntry a, ListingEntry b)
        {
            a._notes = combineNotes(a, b);
            a._priority = (Priority)Math.Max((int)a._priority, (int)b._priority);
            return a;
        }
    }
}
