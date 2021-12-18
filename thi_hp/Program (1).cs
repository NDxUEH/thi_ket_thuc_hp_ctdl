using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System;

namespace MyCS
{
    class Program
    {
        public static void Template()
        {
            Console.WriteLine("***CÂY NHỊ PHÂN TÌM KIẾM***");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Thêm bản tin vào cây nhị phân");
            Console.WriteLine("2. Tìm kiếm bản tin trong cây nhị phân");
            Console.WriteLine("3. Xóa bản tin trong cây nhị phân");
            Console.WriteLine("4. Duyệt tiền thứ tự cây nhị phân");
            Console.WriteLine("5. Đếm số lượng bản tin hiện tại được lưu trữ trong cây nhị phân");
            Console.WriteLine("6. Thoát chương trình");
            Console.WriteLine("=================================");
        }
        public static void SubTemplate1()
        {
            Console.WriteLine("1.Tìm kiếm bản tin theo ID");
            Console.WriteLine("2.Tìm kiếm theo từ khóa tương ứng lựa chọn tiêu đề hoặc nội dung hoặc đánh giá");
            Console.WriteLine("3.Tìm kiếm theo thời gian bài đăng");
            Console.WriteLine("4.Tìm kiếm bài đăng từ mốc thời gian bất kì trở về trước");
            Console.WriteLine("5.Tìm kiếm bài đăng trong khoảng thời gian đầy đủ bất kì");
            Console.WriteLine("6.Tìm theo ngày hoặc tháng hoặc năm bất kỳ");
            Console.WriteLine(@"7.Tìm kiếm kết hợp trong những bản tin thỏa tiêu đề hoặc nội dung hoặc 
đánh giá lấy bài có ngày hoặc tháng hoặc năm hoặc thời gian đầy đủ cũ nhất hoặc mới nhất");
            Console.WriteLine(@"8.Tìm kiếm tin tức có ngày hoặc tháng hoặc năm hoặc thời gian đầy 
đủ (dd/MM/yyyy) cũ hoặc mới nhất");
            Console.WriteLine(@"9.Tìm kiếm kết hợp nội dung hoặc tiêu đề hoặc đánh giá đồng thời có thời gian nằm 
trong khoảng năm; tháng; ngày này -> năm; tháng; ngày kia, tại 1 thời điểm cụ thể, trước 1 thời điểm nào đó");
            Console.WriteLine("10.Thoát chức năng hiện tại");
        }
        static void Main(string[] args)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            BinarySearchTree binaryTree = new BinarySearchTree();
            // thêm bản tin vào cây (mọi người có thể thêm tùy ý, hoặc thay đổi tiêu đề, nội dung bản tin khác. 
            // Chú ý id do cây lưu trữ bằng cách so sánh id)
            binaryTree.Insert(new News(6, "Vietnam re-open", "Flight connections are available across Vietnam as domestic travel resumes", "15/12/2021", "great things will come"));
            binaryTree.Insert(new News(2, "Crash of Boeing 737 Max", "the Boeing 737 MAX operating the route crashed 13 minutes after takeoff, killing all 189 passengers and crew", "29/10/2018", "sad story"));
            binaryTree.Insert(new News(1, "Brexit delayed for a third time", "Boris Johnson replaced May as the prime minister, and requested a Brexit extension until 31/01/2020", "28/10/2019", "It's maybe not a good choice"));
            binaryTree.Insert(new News(3, "President to visit Cambodia", "President Nguyen Xuan Phuc will have an official visit to the Kingdom of Cambodia next week.", "17/12/2021", "good luck"));
            binaryTree.Insert(new News(5, "FED raises interest rates", "The Federal Reserve raised its key interest rate from a range of 0-0.25% to a range of 0.25-0.5%.", "16/12/2015", "good news"));
            binaryTree.Insert(new News(7, "Metaverse, NFT, And DeFi: Here's Why They Matter", "The three entities have their unique uses while also sharing and contributing to each other’s growth", "17/12/2021", "They're a future"));
            binaryTree.Insert(new News(4, "Is Web3 a future of Internet?", "Along with the metaverse, Web 3.0 is expected to become the future of technology.", "01/12/2021", "I know it"));
            binaryTree.Insert(new News(8, "September 11 Attacks", "9 militants associated with the Islamic (al Qaeda) hijacked 4 airplanes and carried out suicide attacks against targets in the US", "25/08/2018", "Pray for people"));
            Template();
            // chạy chức năng
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n-> Vui lòng chọn chức năng: ");
                Console.ResetColor();
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("Nhập id: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Nhập tiêu đề bản tin: ");
                        string title = Console.ReadLine();
                        Console.Write("Nhập nội dung bản tin: ");
                        string content = Console.ReadLine();
                        Console.Write("Nhập ngày bản tin: ");
                        string date = Console.ReadLine();
                        Console.Write("Nhập đánh giá bản tin: ");
                        string comment = Console.ReadLine();
                        binaryTree.Insert(new News(id, title, content, date, comment));
                        Console.WriteLine("Thêm thành công");
                        break;
                    case 2:
                        Console.WriteLine("\n************************************************************************\n");
                        SubTemplate1();
                        Console.WriteLine("\n************************************************************************");
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\n-> Nhập chức năng: ");
                            Console.ResetColor();
                            int choice1 = int.Parse(Console.ReadLine());
                            Console.WriteLine("");
                            switch (choice1)
                            {
                                case 1:
                                    Console.Write("Nhập ID bản tin cần tìm: ");
                                    int choice_id = int.Parse(Console.ReadLine());
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindId(choice_id);
                                    break;
                                case 2:
                                    Console.Write("Với 0-tiêu đề, 1-nội dung, 2-đánh giá, nhập loại tìm kiếm muốn chọn: ");
                                    int option = int.Parse(Console.ReadLine());
                                    Console.Write("\nNhập từ khóa tìm kiếm tương ứng với lựa chọn trước: ");
                                    string kword = Console.ReadLine();
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindKeyword(option, kword);
                                    break;
                                case 3:
                                    Console.Write("Nhập thời gian dưới dạng (dd/MM/yyyy) của bản tin muốn tìm kiếm: ");
                                    string date_str = Console.ReadLine();
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindDate(date_str);
                                    
                                    break;
                                case 4:
                                    Console.Write("Nhập mốc thời gian dưới dạng (dd/MM/yyyy) của bản tin muốn tìm kiếm trước đó: ");
                                    string date_bf = Console.ReadLine();
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindDateBefore(date_bf);
                                    break;
                                case 5:
                                    Console.Write("Nhập thời gian bắt đầu dưới dạng (dd/MM/yyyy): ");
                                    string fr_date = Console.ReadLine();
                                    Console.Write("\nNhập thời gian kết thúc dưới dạng (dd/MM/yyyy) : ");
                                    string to_date = Console.ReadLine();
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindDateInterval(fr_date, to_date);
                                    break;
                                case 6:
                                    Console.Write("Với 0-ngày, 1-tháng, 2-năm, nhập loại tìm kiếm muốn chọn: ");
                                    int i = int.Parse(Console.ReadLine());
                                    Console.Write("\nNhập giá trị cụ thể tương ứng với sự lựa chọn đó: ");
                                    string specific_ele = Console.ReadLine();
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindElement(i, specific_ele);
                                    break;
                                case 7:
                                    Console.Write("Với 0-tiêu đề, 1-nội dung, 2-đánh giá, nhập loại tìm kiếm muốn chọn: ");
                                    int option1 = int.Parse(Console.ReadLine());
                                    Console.Write("\nNhập từ khóa tìm kiếm tương ứng với lựa chọn trước: ");
                                    string kword1 = Console.ReadLine();
                                    Console.Write("\nVới  0-ngày, 1-tháng, 2-năm, 3-thời gian đầy đủ (dd//MM/yyyy), nhập lựa chọn: ");
                                    int option2 = int.Parse(Console.ReadLine());
                                    Console.Write("\nVới 0-cũ nhất, 1-mới nhất, nhập lựa chọn: ");
                                    int option_3 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindCombineElementOption(option1, kword1, option2, option_3);
                                    break;
                                case 8:
                                    Console.Write("Với  0-ngày, 1-tháng, 2-năm, 3-thời gian đầy đủ (dd//MM/yyyy), nhập lựa chọn: ");
                                    int option3 = int.Parse(Console.ReadLine());
                                    Console.Write("\nVới 0-cũ nhất, 1-mới nhất, nhập lựa chọn: ");
                                    int option_4 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindDateOption(option3, option_4);
                                    break;
                                case 9:
                                    Console.Write("Với 0-tiêu đề, 1-nội dung, 2-đánh giá, nhập loại tìm kiếm muốn chọn: ");
                                    int optionA = int.Parse(Console.ReadLine());
                                    Console.Write("\nNhập từ khóa tìm kiếm tương ứng với lựa chọn trước: ");
                                    string kwordA = Console.ReadLine();
                                    Console.Write("\nVới  0-ngày, 1-tháng, 2-năm, 3-thời gian đầy đủ (dd//MM/yyyy), nhập lựa chọn: ");
                                    int optionB = int.Parse(Console.ReadLine());
                                    Console.Write("\nNhập giá trị bắt đầu ứng với lựa chọn: ");
                                    string start_ele = Console.ReadLine();
                                    Console.Write("\nNhập giá trị kết thúc ứng với lựa chọn: ");
                                    string end_ele = Console.ReadLine();
                                    Console.WriteLine("\nKết quả tìm kiếm: \n");
                                    binaryTree.FindCombineElement(optionA, kwordA, optionB, start_ele, end_ele);
                                    break;
                                case 10:
                                    goto thoat;
                                default:
                                    Console.WriteLine("Nhập sai chức năng");
                                    break;
                            }
                        thoat:;
                            if (choice1 == 10)
                                break;
                        }
                        continue;
                    case 3:
                        Console.WriteLine("Nhập ID bản tin muốn xóa: ");
                        int id_remove = int.Parse(Console.ReadLine());
                        binaryTree.RemoveNode(id_remove);
                        Console.WriteLine("Xóa thành công");
                        break;
                    case 4:
                        Console.WriteLine("Kết quả duyệt tiền thứ tự cây nhị phân: ");
                        binaryTree.TraversePreOrder(binaryTree.Root);
                        break;
                    case 5:
                        Console.WriteLine("Số lượng bản tin lưu trữ trong cây nhị phân là: " + binaryTree.CountNode(binaryTree.Root));
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Nhập sai chức năng");
                        break;
                }
                if (choice == 6)
                    break;
            }
            Console.WriteLine("Đã thoát chương trình"); 
            
        }
    }
}
