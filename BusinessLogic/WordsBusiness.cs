using EntityContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class WordsBusiness : BusinessBase
    {
        private int _userId;
        public WordsBusiness(TestContext context) : base(context)
        {
        }

        public void AddToDb(string ip, string words)
        {
            AddNewUser(ip);
            AddWordsToDb(words);
        }

        public List<Word> GetTable()
        {
            List<Word> res = new List<Word>();
            res = Database.Words.Where(x => x.UserId == _userId).OrderByDescending(x => x.Count).ThenBy(x => x.Word1).ToList();

            return res;
        }

        private Dictionary<string, int> GetWords(string words)
        {
            Dictionary<string, int> dicword = new Dictionary<string, int>( StringComparer.InvariantCultureIgnoreCase); 
            string[] wordsList = Regex.Split(words, @"\W|_");
            if (wordsList.Length > 0)
            {

                for (int i = 0; i < wordsList.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(wordsList[i]) && wordsList[i].Length > 3)
                    {
                        dicword[wordsList[i]] = (dicword.ContainsKey(wordsList[i])) ? ++dicword[wordsList[i]] : 1;
                    }
                }
            }
            return dicword;
        }

        private void AddNewUser(string ip)
        {
            User u = new User() { Ip = ip };
            Database.Users.Add(u);
            Database.SaveChanges();

            if (u != null)
                _userId = u.Id;
        }

        private void AddWordsToDb(string words)
        {

            Dictionary<string, int> d = GetWords(words);
            if (d.Count > 0)
            {
                foreach (var itm in d)
                {
                    Word w = new Word
                    {
                        Count = itm.Value,
                        Word1 = itm.Key,
                        UserId = _userId
                    };
                    Database.Words.Add(w);
                }
                Database.SaveChanges();
            }
        }
    }
}
