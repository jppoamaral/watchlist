using System;

namespace Watchlist
{
    class Program
    {
		static MovieRepository movieRep = new MovieRepository();
		static SeriesRepository seriesRep = new SeriesRepository();

        static void Main(string[] args)
        {
            string userCommand = getUserCommand();

			while (userCommand != "X")
			{
				switch (userCommand)
				{
					case "1":
						List();
						break;
					case "2":
						Insert();
						break;
					case "3":
						Update();
						break;
					case "4":
						Remove();
						break;
					case "5":
						Search();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				userCommand = getUserCommand();
			}

			Console.WriteLine("Thank you for using Amaral's Watchlist services.");
			Console.ReadLine();
        }

        private static void Remove()
		{
			Console.Write("Insert id to remove: ");
			int idToRemove = int.Parse(Console.ReadLine());
			Console.Write("\nAre you removing a (1)movie or (2)series: ");
			int itemType = int.Parse(Console.ReadLine());
			Console.WriteLine();
			if (itemType == 1)
			{
				var movie = movieRep.returnById(idToRemove);
				movie.Remove();
			}
			else if (itemType == 2)
			{
				var series = seriesRep.returnById(idToRemove);
				series.Remove();
			}
			Console.WriteLine("Successful Remove!");
		}

        private static void Search()
		{
			Console.Write("Searching for (1)movie or (2)series? ");
			int choice = int.Parse(Console.ReadLine());
			Console.Write("Insert id to search: ");
			int idToSearch = int.Parse(Console.ReadLine());
			if (choice == 1)
			{
				var movie = movieRep.returnById(idToSearch);
				Console.WriteLine(movie);
			}
			else if (choice == 2)
			{
				var series = seriesRep.returnById(idToSearch);
				Console.WriteLine(series);
			}
		}

        private static void Update()
		{
			Console.WriteLine("Inform the desired option");
			Console.WriteLine("(1) Update the status of an item");
			Console.WriteLine("(2) Update an item");
			int decision = int.Parse(Console.ReadLine());
			Console.WriteLine(); //pula linha
			switch (decision)
			{
				case 1:
					Console.Write("Insert id to set item as watched: ");
					int idToWatch = int.Parse(Console.ReadLine());
					Console.Write("\nIs it a (1)movie or a (2)series: ");
					int itemType = int.Parse(Console.ReadLine());
					if (itemType == 1)
					{
						var movie = movieRep.returnById(idToWatch);
						movie.Watch();
					}	
					else if (itemType == 2)
					{
						var series = seriesRep.returnById(idToWatch);
						series.Watch();
					}
					break;
				case 2:
					Console.WriteLine("Do you want to update a movie or a series? ");
					Console.WriteLine("(1) Movie");
					Console.WriteLine("(2) Series");
					int decision1 = int.Parse(Console.ReadLine());
					Console.WriteLine(); //pula linha
					Console.Write("Insert Id: ");
					int newId = int.Parse(Console.ReadLine());

					Console.Write("Insert Title: ");
					string newTitle = Console.ReadLine();

					Console.WriteLine(); //pula linha
					foreach (int i in Enum.GetValues(typeof(Genre)))
					{
						Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genre), i));
					}
					Console.WriteLine();

					Console.Write("Insert one of the Genres above: ");
					int newGenre = int.Parse(Console.ReadLine());

					Console.Write("Insert Release Year: ");
					int newYear = int.Parse(Console.ReadLine());

					Console.Write("Insert Description: ");
					string newDescrpt = Console.ReadLine();
				
					switch (decision1)
					{
						case 1:
							Movie updateMovie = new Movie(id: newId,
										title: newTitle,
										genre: (Genre)newGenre,
										year: newYear,
										descrpt: newDescrpt);
							movieRep.Update(newId, updateMovie);		
							break;
						case 2:
							Series updateSeries = new Series(id: newId,
										title: newTitle,
										genre: (Genre)newGenre,
										year: newYear,
										descrpt: newDescrpt);
							seriesRep.Update(newId, updateSeries);		
							break;
					}
					break;
			}
			Console.WriteLine("Successful Update!");
		}
        private static void List()
		{
            Console.WriteLine("Do you want to list movies or series? ");
            Console.WriteLine("(1) Movies");
            Console.WriteLine("(2) Series");
			Console.WriteLine("(3) List All");
            int decision = int.Parse(Console.ReadLine());
			Console.WriteLine(); //pula linha

			var lst1 = movieRep.List();
			var lst2 = seriesRep.List();
			if (lst1.Count + lst2.Count == 0)
            {
                Console.WriteLine("Nothing was registered.");
                return;
            }

            switch (decision)
            {
                case 1:
                    Console.WriteLine("List movies");

                    foreach (var movie in lst1)
                    {
						bool watched = movie.returnWatched();
						bool removed = movie.returnRemoved();
                        Console.WriteLine("#ID {0}: - {1} {2} {3}", movie.returnId(), movie.returnTitle(), (watched ? "*Watched*" : ""), (removed ? "*Removed*" : ""));
                    }
                    break;
                case 2:
                    Console.WriteLine("List series");

                    foreach (var series in lst2)
                    {
 						bool watched = series.returnWatched();
						bool removed = series.returnWatched();
                        Console.WriteLine("#ID {0}: - {1} {2} {3}", series.returnId(), series.returnTitle(), (watched ? "*Watched*" : ""), (removed ? "*Removed*" : "")); 
                    }
                    break;
				case 3:
					Console.WriteLine("List All");
					
					Console.WriteLine("\n---MOVIES---");
					if (lst1.Count == 0)
						Console.WriteLine("No movies were registered");
					foreach (var movie in lst1)
                    {
						bool watched = movie.returnWatched();
						bool removed = movie.returnRemoved();
                        Console.WriteLine("#ID {0}: - {1} {2} {3}", movie.returnId(), movie.returnTitle(), (watched ? "*Watched*" : ""), (removed ? "*Removed*" : ""));
                    }
					Console.WriteLine("\n---SERIES---");
					if (lst2.Count == 0)
						Console.WriteLine("No series were registered");
                    foreach (var series in lst2)
                    {
 						bool watched = series.returnWatched();
						bool removed = series.returnWatched();
                        Console.WriteLine("#ID {0}: - {1} {2} {3}", series.returnId(), series.returnTitle(), (watched ? "*Watched*" : ""), (removed ? "*Removed*" : "")); 
                    }
					break;
            }
		}

        private static void Insert()
		{
			Console.WriteLine("Do you want to insert a movie or a series? ");
            Console.WriteLine("(1) Movie");
            Console.WriteLine("(2) Series");
            int decision = int.Parse(Console.ReadLine());
			Console.WriteLine(); //pula linha

			Console.Write("Insert Title: ");
			string newTitle = Console.ReadLine();

			Console.WriteLine(); //pula linha
			foreach (int i in Enum.GetValues(typeof(Genre)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genre), i));
			}
			Console.WriteLine();

			Console.Write("Insert one of the Genres above: ");
			int newGenre = int.Parse(Console.ReadLine());

			Console.Write("Insert Release Year: ");
			int newYear = int.Parse(Console.ReadLine());

			Console.Write("Insert Description: ");
			string newDescrpt = Console.ReadLine();
			
			switch (decision)
			{
				case 1:
					Movie newMovie = new Movie(id: movieRep.nextId(),
								title: newTitle,
								genre: (Genre)newGenre,
								year: newYear,
								descrpt: newDescrpt);
					movieRep.Insert(newMovie);		
					break;
				case 2:
					Series newSeries = new Series(id: seriesRep.nextId(),
								title: newTitle,
								genre: (Genre)newGenre,
								year: newYear,
								descrpt: newDescrpt);
					seriesRep.Insert(newSeries);	
					break;
			}		
		}

        private static string getUserCommand()
		{
			Console.WriteLine();
			Console.WriteLine("Watchlist - Main Menu");
			Console.WriteLine("Inform the desired option:");

			Console.WriteLine("1- List movies / series");
			Console.WriteLine("2- Insert new movie / series");
			Console.WriteLine("3- Update");
			Console.WriteLine("4- Remove");
			Console.WriteLine("5- Search for movie / series");
			Console.WriteLine("C- Clear Window");
			Console.WriteLine("X- EXIT");
			Console.WriteLine();

			string userOp = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return userOp;
		}
    }
}
