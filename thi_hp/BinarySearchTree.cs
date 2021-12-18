using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Globalization;
namespace MyCS
{
    public class BinarySearchTree
    {
        public Node Root { get; set; }
        public int count = 0;

        // thêm bản tin vào cây nhị phân bằng cách so sánh id 
        public bool Insert(News ban_tin)
        {
            Node before = null, after = this.Root; 
            while (after != null)
            {
                before = after;
                // so sánh id của bản tin với id của node hiện tại
                if (ban_tin.getId() < (after.Data.getId()))
                    after = after.LeftNode;
                else if (ban_tin.getId() > after.Data.getId())
                    after = after.RightNode;
                else
                    return false;
            }

            Node newNode = new Node();
            newNode.Data = ban_tin;
            if (this.Root == null)
                this.Root = newNode;
            else {
                if (ban_tin.getId() < before.Data.getId())
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }
            return true;
            
        } 

        // Duyệt tiền thứ tự của cây nhị phân
        public void TraversePreOrder(Node parent){
            if (parent != null){
                Console.WriteLine(parent.Data);
                TraversePreOrder(parent.LeftNode);
                TraversePreOrder(parent.RightNode);              
            }
        }

        
        //Đếm Node
        public int CountNode(Node root)
        {
            if (root == null) return 0;
            return (1 + CountNode(root.LeftNode) + CountNode(root.RightNode));
        }

        //Xóa Node
        public void RemoveNode(int key)
        {
            Remove(this.Root, key);
            count++;

        }
        private Node Remove(Node root, int val)
        {
            if (root == null) return root;
            if (val < root.Data.getId()) root.LeftNode = Remove(root.LeftNode, val);
            else if (val > root.Data.getId())
            {
                root.RightNode = Remove(root.RightNode, val);
            }
            else
            {
                if (root.LeftNode == null && root.RightNode == null)
                {
                    root = null;
                }    
                else if (root.LeftNode != null && root.RightNode != null)
                {
                    Node max = FindMax(root.RightNode);
                    root.Data = max.Data;
                    root.RightNode = Remove(root.RightNode, max.Data.getId());
                }    
                else
                {
                    Node child = root.LeftNode != null ? root.LeftNode : root.RightNode;
                    root = child;
                }    
            }
            return root;
        }
        private Node FindMax(Node node)
        {
            while (node.LeftNode != null)
            {
                node = node.LeftNode;
            }
            return node;
        }

        // Lấy thời gian nhỏ nhất trong cây
        public DateTime getMinDate()
        {
            List<DateTime> list = new List<DateTime>();
            this.AddDate(list, this.Root);      
            list.Sort((a, b) => a.CompareTo(b));      // sắp xếp ngày tăng dần bắt đầu từ ngày cũ nhất
            list = list.Distinct().ToList();          // loại bỏ các ngày trùng nhau sử dụng System.Linq hỗ trợ
            return list[0];
        }
        
        // Chức năng 1: Tìm kiếm theo id
        public void FindId(int id)
        {
            List<Node> list = new List<Node>();
            this.Find(list, id, this.Root);
            foreach (Node node in list)
            {
                Console.WriteLine(node.Data);
            }
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy bản tin có thời gian này");
        }
        private void Find(List<Node> list, int id, Node parent)
        {
            if (parent != null)
            {
                if (id == parent.Data.getId())
                {
                    list.Add(parent);
                }    
                Find(list, id, parent.LeftNode);
                Find(list, id, parent.RightNode);
            }
        }
        // Chức năng 2: Tìm kiếm theo từ khóa với lựa chọn dựa trên tiêu đề hoặc nội dung hoặc đánh giá
        public void FindKeyword(int option, string kword){
            // option: 0-tiêu đề, 1-nội dung, 2-đánh giá
            // kword: từ khóa hoặc đoạn văn muốn tìm kiếm theo option đã lựa chọn
            List<Node> list = new List<Node>();
            this.FindK(list, option, kword, this.Root);
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy bản tin phù hợp với từ khóa");
            foreach (Node node in list){
                Console.WriteLine(node.Data);
            }        
        }
        private void FindK(List<Node> list, int option, string kword, Node parent){
            if (parent != null){
                if (option == 0){
                    if (parent.Data.getTitle().ToLower().Contains(kword.ToString()))
                        list.Add(parent);
                }
                else if (option == 1){
                    if (parent.Data.getContent().ToLower().Contains(kword.ToString()))
                        list.Add(parent);
                }
                else if (option == 2){
                    if (parent.Data.getComments().ToString().ToLower().Contains(kword.ToString()))
                        list.Add(parent);
                }
                // đệ quy 
                FindK(list, option, kword, parent.LeftNode);
                FindK(list, option, kword, parent.RightNode);
            }
        }

        // Chức năng 3: Tìm kiếm theo thời gian bài đăng
        public void FindDate(string date){
            List<Node> list = new List<Node>();
            this.FindD(list, date, this.Root);
            foreach (Node node in list){
                Console.WriteLine(node.Data);
            }
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy bản tin có thời gian này");
        }

        private void FindD(List<Node> list, string date, Node parent){
            if (parent != null){
                if (parent.Data.getDatePublish().Contains(date.Trim()))
                    list.Add(parent);
                // đệ quy 
                FindD(list, date, parent.LeftNode);
                FindD(list, date, parent.RightNode);
            }
        }
        

        // Chức năng 5: Tìm kiếm bài đăng từ mốc thời gian bất kì trở về trước
        public void FindDateBefore(string date){
            List<Node> list = new List<Node>();
            this.FindDB(list, date, this.Root);
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy bản tin có thời gian này");
            foreach (Node node in list){
                Console.WriteLine(node.Data);
            }
            
        }
        
        private void FindDB(List<Node> list, string date, Node parent){
            if (parent != null){
                DateTime dt = DateTime.ParseExact(parent.Data.getDatePublish(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (DateTime.Compare(dt,DateTime.ParseExact(date.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) <= 0)
                    list.Add(parent);
                // đệ quy 
                FindDB(list, date, parent.LeftNode);
                FindDB(list, date, parent.RightNode);
            }
        }

        //Chức năng 6: Tìm kiếm bài đăng trong khoảng thời gian đầy đủ bất kì
        public void FindDateInterval(string fr_date, string to_date){
            // fr_date: ngày bắt đầu, 
            // to_date: ngày kết thúc
            List<Node> list = new List<Node>();
            this.FindDI(list, fr_date, to_date, this.Root);
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy bản tin có thời gian này");
            foreach (Node node in list){
                Console.WriteLine(node.Data);
            }
            
        }
        
        private void FindDI(List<Node> list, string fr_date, string to_date, Node parent){
            if (parent != null){
                DateTime dt = DateTime.ParseExact(parent.Data.getDatePublish(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (DateTime.Compare(dt,DateTime.ParseExact(to_date.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) <= 0 && DateTime.Compare(dt,DateTime.ParseExact(fr_date.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) >= 0)
                    list.Add(parent);
                // đệ quy 
                FindDI(list, fr_date, to_date, parent.LeftNode);
                FindDI(list, fr_date, to_date,  parent.RightNode);
            }
        }

        //Chức năng 7: Tìm theo ngày hoặc tháng hoặc năm bất kỳ
        public void FindElement(int i, string specific_ele)
        {
            // i: 0-ngày, 2-tháng, 3-năm
            /* specific_ele: 
                nếu i=0 thì specific_ele nhập ngày bất kì, 
                    i=2 thì specific_ele nhập tháng bất kì,
                    i=3 thì specific_ele nhập năm bất kì*/
            List<Node> list = new List<Node>();
            FindEle(list, i, specific_ele, this.Root);
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy bản tin phù hợp");
            foreach (Node node in list)
                Console.WriteLine(node.Data);
            
        }
        private void FindEle(List<Node> list, int i, string specific_ele, Node parent)
        {
            if (parent != null)
            {
                String[] stryr = parent.Data.getDatePublish().Split('/');
                if (int.Parse(stryr[i]) == int.Parse(specific_ele))
                {
                    list.Add(parent);
                }
                FindEle(list, i, specific_ele, parent.LeftNode);
                FindEle(list, i, specific_ele, parent.RightNode);
            }
        }

        /* Chức năng 8: Tìm kiếm kết hợp trong những bản tin thỏa tiêu đề hoặc nội dung hoặc đánh giá lấy bài
         có ngày hoặc tháng hoặc năm hoặc thời gian đầy đủ cũ nhất hoặc mới nhất*/
        public void FindCombineElementOption(int i1, string kword, int i2, int option){
            // i1 có lựa chọn 0-tiêu đề, 1-nội dung, 2-đánh giá
            // kword: nhập từ khóa tương ứng với lựa chọn i1
            // i2 có lựa chọn 0-ngày, 1-tháng, 2-năm, 3-thời gian đầy đủ (dd//MM/yyyy)
            // option: 0-cũ nhất, 1-mới nhất
            List<Node> list = new List<Node>();
            FindCEO(list, i1, kword, this.Root);
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy bản tin phù hợp");
            else
                if (i2 != 3){
                    List<int> listtime = new List<int>();
                    for (int i = 0; i < list.Count; i++)
                        listtime.Add(int.Parse(list[i].Data.getDatePublish().Split("/")[i2]));  

                    listtime.Sort();      // sắp xếp tăng dần
                    listtime = listtime.Distinct().ToList();          // loại bỏ các năm trùng nhau sử dụng System.Linq hỗ trợ
                    if (option == 0)
                        foreach (Node node in list){
                            if (int.Parse(node.Data.getDatePublish().Split("/")[i2]) == listtime[0]) // phần tử đầu tiên là phần tử nhỏ nhất
                                Console.WriteLine(node.Data);
                        }            
                    else
                        foreach (Node node in list){
                            if (int.Parse(node.Data.getDatePublish().Split("/")[i2]) == listtime[listtime.Count - 1]) // phần tử cuối cùng là phần tử lớn nhất
                                Console.WriteLine(node.Data);
                        }
                }
                else if (i2 == 3){
                    List<DateTime> listtime = new List<DateTime>();
                    for (int i = 0; i < list.Count; i++)
                        listtime.Add(DateTime.ParseExact(list[i].Data.getDatePublish(), "dd/MM/yyyy", CultureInfo.InvariantCulture));  
                    listtime.Sort((a, b) => a.CompareTo(b));      // sắp xếp tăng dần
                    listtime = listtime.Distinct().ToList();          // loại bỏ các năm trùng nhau sử dụng System.Linq hỗ trợ
                    if (option == 0)
                        foreach (Node node in list){
                            if (node.Data.getDatePublish() == listtime[0].ToString("dd/MM/yyyy")) // phần tử đầu tiên là phần tử nhỏ nhất
                                Console.WriteLine(node.Data);
                        }            
                    else if (option == 1)
                        foreach (Node node in list){
                            if (node.Data.getDatePublish() == listtime[listtime.Count - 1].ToString("dd/MM/yyyy")) // phần tử cuối cùng là phần tử lớn nhất
                                Console.WriteLine(node.Data);
                        }
                }
        }
        private void FindCEO(List<Node> list, int i1, string kword, Node parent){
            if (parent != null){
                bool boolCond = false;
                if (i1 == 0) 
                {
                    boolCond = parent.Data.getTitle().Trim().ToLower().Contains(kword.Trim().ToLower());
                }
                else if (i1 == 1)
                {
                    boolCond = parent.Data.getContent().Trim().ToLower().Contains(kword.Trim().ToLower());
                }
                else if (i1 == 2)
                {
                    boolCond = parent.Data.getComments().Trim().ToLower().Contains(kword.Trim().ToLower());
                } 
                if (boolCond){
                    list.Add(parent);
                }
                FindCEO(list, i1, kword, parent.LeftNode);
                FindCEO(list, i1, kword, parent.RightNode);
            }
        }

        
        
        //Chức năng 9: Tìm kiếm tin tức có ngày hoặc tháng hoặc năm hoặc thời gian đầy đủ (dd/MM/yyyy) cũ hoặc mới nhất
        public void FindDateOption(int option1, int option2){              
            // option1 là lựa chọn cần tìm (0-ngày, 1-tháng, 2-năm, 3-thời gian đầy đủ(dd/MM/yyyy))
            // option2: 0-cũ nhất, 1-mới nhất
            if (option1 != 3){
                List<int> list = new List<int>();
                this.AddElement(list, option1, this.Root);    
                list.Sort();      // sắp xếp ngày tăng dần bắt đầu từ năm cũ nhất
                list = list.Distinct().ToList();          // loại bỏ các năm trùng nhau sử dụng System.Linq hỗ trợ
                if (option2 == 0)
                    FindElement(option1, list[0].ToString());             // phần tử đầu tiên là phần tử nhỏ nhất tức là năm cũ nhất
                else if (option2 == 1)
                    FindElement(option1, list[list.Count - 1].ToString()); // phần tử cuối cùng là phần tử lớn nhất tức là năm mới nhất
            }
            else{
                List<DateTime> list = new List<DateTime>();
                this.AddDate(list, this.Root);      
                list.Sort((a, b) => a.CompareTo(b));      // sắp xếp ngày tăng dần bắt đầu từ ngày cũ nhất
                list = list.Distinct().ToList();          // loại bỏ các ngày trùng nhau sử dụng System.Linq hỗ trợ
                if (option2 == 0)
                    FindDate(list[0].ToString("dd/MM/yyyy"));             // phần tử đầu tiên là phần tử nhỏ nhất tức là ngày cũ nhất
                else if (option2 == 1)
                    FindDate(list[list.Count - 1].ToString("dd/MM/yyyy")); // phần tử cuối cùng là phần tử lớn nhất tức là ngày mới nhất           
            }
        }
        // i == 3
        private void AddDate(List<DateTime> list, Node parent){
            if (parent != null){
                list.Add(DateTime.ParseExact(parent.Data.getDatePublish(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                // đệ quy duyệt cây
                AddDate(list, parent.LeftNode);
                AddDate(list, parent.RightNode);
            }    
        }
        // i != 3
        private void AddElement(List<int> list, int i, Node parent){
            if (parent != null){
                list.Add(int.Parse(parent.Data.getDatePublish().Split('/')[i]));
                // đệ quy duyệt cây
                AddElement(list, i, parent.LeftNode);
                AddElement(list, i, parent.RightNode);
            }    
        }

        /* Chức năng 10: Tìm kiếm kết hợp nội dung hoặc tiêu đề hoặc đánh giá đồng thời có thời gian nằm 
        trong khoảng năm này -> năm kia, tháng này -> tháng kia, ......
        VD:
        - Nếu muốn tìm TẠI MỘT THỜI ĐIỂM CỤ THỂ thì nhập starEle = endEle
        - Nếu muốn tìm TRƯỚC thời điểm cụ thể thì nhập starEle = getMinDate() hoặc ngày, tháng, năm nhỏ nhất
          và endEle là thời điểm cụ thể
        */    
        public void FindCombineElement(int i1, string kword, int i2, string startEle, string endEle)   
        {
            // i1 có lựa chọn 0-tiêu đề, 1-nội dung, 2-đánh giá
            // kword: nhập từ khóa tương ứng với lưa chọn i1
            // i2 có lựa chọn 0-năm, 1-tháng, 2-ngày, 3-thời gian đầy đủ (dd//MM/yyyy)
            // startEle, endEle: nhập tương ứng với lựa chọn i2
            List<Node> list = new List<Node>();
            FindCE(list, i1, kword, i2, startEle, endEle, this.Root);
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy bản tin phù hợp");
            foreach (Node node in list)
            {
                Console.WriteLine(node.Data);
            }         
        }
        private void FindCE(List<Node> list, int i1, string kword, int i2, string startEle, string endEle, Node parent)
        {
            if (parent != null)
            {
                bool boolCond = false;
                if (i1 == 0) 
                {
                    boolCond = parent.Data.getTitle().Trim().ToLower().Contains(kword.Trim().ToLower());
                }
                else if (i1 == 1)
                {
                    boolCond = parent.Data.getContent().Trim().ToLower().Contains(kword.Trim().ToLower());
                }
                else if (i1 == 2)
                {
                    boolCond = parent.Data.getComments().Trim().ToLower().Contains(kword.Trim().ToLower());
                }
                
                if (i2 != 3){
                    String[] strEle = parent.Data.getDatePublish().Split('/');
                    if (int.Parse(strEle[i2]) >= int.Parse(startEle) && int.Parse(strEle[i2]) <= int.Parse(endEle) 
                        && boolCond)
                    {
                        list.Add(parent);
                    }
                    FindCE(list, i1, kword, i2, startEle, endEle, parent.LeftNode);
                    FindCE(list, i1, kword, i2, startEle, endEle, parent.RightNode);
                }
                else{
                    
                    DateTime dtime = DateTime.ParseExact(parent.Data.getDatePublish(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if ((DateTime.Compare(dtime,DateTime.ParseExact(startEle.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) >= 0 
                        && DateTime.Compare(dtime, DateTime.ParseExact(endEle.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) <= 0)
                        && boolCond)
                    {
                        list.Add(parent);
                    }
                    FindCE(list, i1, kword, i2, startEle, endEle, parent.LeftNode);
                    FindCE(list, i1, kword, i2, startEle, endEle, parent.RightNode);
                }
            }
        }        
    }
}
