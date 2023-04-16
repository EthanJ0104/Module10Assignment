using NLog;
using System.Linq;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
var db = new BloggingContext();
string choice = "";
logger.Info("Program started");

try
{
    do
    {
        Console.WriteLine("1) Display all blogs");
        Console.WriteLine("2) Add blog");
        Console.WriteLine("3) Create post");
        Console.WriteLine("4) Display all posts");
        Console.WriteLine("q to quit");
        Console.Write("> ");
        choice = Console.ReadLine();

        if (choice == "1")
        {
            // Display all Blogs from the database
            var query = db.Blogs.OrderBy(b => b.Name);

            Console.WriteLine("All blogs in the database:");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
        }

        else if (choice == "2")
        {
            // Create and save a new Blog
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();

            var blog = new Blog { Name = name };

            
            db.AddBlog(blog);
            logger.Info("Blog added - {name}", name);
        }

        else if (choice == "3")
        {
            var query = db.Blogs.OrderBy(b => b.Name);

            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
            
            Console.Write("Enter Blog to post under: ");
            var blog = Console.ReadLine();

            Console.Write("Post title: ");
            var title = Console.ReadLine();

            Console.Write("Post content: ");
            var content = Console.ReadLine();

            var post = new Post { Title = title, Content = content };
            db.AddPost(post);
        }

        else if (choice == "4")
        {

        }

        else 
        {
            Console.WriteLine("Invalid Input");
        }
    } while (choice != "q");
}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

logger.Info("Program ended");
