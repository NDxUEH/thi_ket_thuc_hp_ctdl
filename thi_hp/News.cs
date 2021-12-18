namespace MyCS
{
    public class News
    {
        private int id;
        private string title;
        private string content;
        private string datePublish;
        private string comments;
        public int getId(){
            return id;
        }
        public string getTitle(){
            return title;
        }
        public string getContent(){
            return content;
        }
        public string getDatePublish(){
            return datePublish;
        }
        public string getComments(){
            return comments;
        }
         public News(int id, string title, string content, string datePublish, string comments){
            this.id = id;
            this.title = title;
            this.content = content;
            this.datePublish = datePublish;
            this.comments = comments;
         }
        override public string ToString(){
            return "News(" + id.ToString() + ", " + title + ", " + content + ", " + datePublish + ", " + comments + ")";
        }
    }
}