using ClassRifle;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Redactor
{
	class TextRedactor
	{

		public static Rifle AK74 = new Rifle("AK-74", "5.45x39mm", "30 patrons");

		public static Rifle HKG36 = new Rifle("HK G36", "5.56x45мм", "30 патронов");
		//public static Rifle SIGSG550 = new Rifle("SIG SG 550", "5.56x45мм", "20 патронов");
		
		public static void init()
		{
			//AK74.name = "AK-74";
			//AK74.caliber = "5.45x39";
			//AK74.patrons_count = "30 патронов";
			List<Rifle> r;
			List<Rifle> rifles = new List<Rifle>{};
			rifles.Add(AK74);
			rifles.Add(HKG36);
			int opcoord = 1;
			//Console.WriteLine(AK74.name);
			Console.WriteLine("Введите путь до файла. Нажмите F2, чтобы перейти в режим редактирования:");
			//XmlSerializer xml = new XmlSerializer(typeof(Rifle));
			string path = Console.ReadLine();
			if (File.Exists(path))
			{
				if (Path.GetExtension(path) == ".xml")
				{
					XmlSerializer xml = new XmlSerializer(typeof(List<Rifle>));
					using (FileStream fs = new FileStream(path, FileMode.Open))
					{
						r = (List<Rifle>)xml.Deserialize(fs);
					}
					//Console.WriteLine(String.Join("\n", r));
					Console.WriteLine($"{r[0].name}\n{r[0].caliber}\n{r[0].patrons_count}\n{r[1].name}\n{r[1].caliber}\n{r[1].patrons_count}");

				}
				else if(Path.GetExtension(path) == ".json")
				{
					string con = File.ReadAllText(path);
					List<Rifle> json_ = JsonConvert.DeserializeObject<List<Rifle>>(con);
					Console.WriteLine($"{json_[0].name}\n{json_[0].caliber}\n{json_[0].patrons_count}\n{json_[1].name}\n{json_[1].caliber}\n{json_[1].patrons_count}");
				}
				else if(Path.GetExtension(path) == ".txt")
				{
					string txt = File.ReadAllText(path);
					Console.WriteLine(txt);
				}
				ConsoleKeyInfo k = Console.ReadKey();
				if (k.Key == ConsoleKey.F2)
				{
				Console.Clear();		
				while(true)
				{
					Console.WriteLine("Выберите объект и нажмите Enter");
					Console.WriteLine("( ) Объект АК-74\n( ) Объект HK G36");
					Console.SetCursorPosition(1, opcoord);
					Console.WriteLine("~");
					ConsoleKeyInfo key = Console.ReadKey();
					if (key.Key == ConsoleKey.UpArrow)
					{
						if (opcoord - 1 >= 0)
						{
							opcoord--;
						}
					}
					else if(key.Key == ConsoleKey.DownArrow)
					{
						if (opcoord + 1 < 3)
						{
							opcoord++;
						}
					}
					else if(key.Key == ConsoleKey.Enter)
					{
						Console.Clear();
						Console.WriteLine("Для сохранения нажмите F1, для выхода Escape");

						if (opcoord == 1)
						{
							Console.WriteLine("Введите новое имя: ");
							AK74.name = Console.ReadLine();
							Console.WriteLine("Введите новый калибр:");
							AK74.caliber = Console.ReadLine();
							Console.WriteLine("Введите новое количество патронов:");
							AK74.patrons_count = Console.ReadLine();
						}
						else if (opcoord == 2)
						{
							Console.WriteLine("Введите новое имя: ");
							HKG36.name = Console.ReadLine();
							Console.WriteLine("Введите новый калибр:");
							HKG36.caliber = Console.ReadLine();
							Console.WriteLine("Введите новое количество патронов:");
							HKG36.patrons_count = Console.ReadLine();
						}

					Console.WriteLine("Введите клавишу");
					key = Console.ReadKey();
					if (key.Key == ConsoleKey.F1)
					{

						if (Path.GetExtension(path) == ".xml")
						{
							XmlSerializer xml = new XmlSerializer(typeof(List<Rifle>));
							using (FileStream fs =  new FileStream(path, FileMode.OpenOrCreate))
							{
								xml.Serialize(fs, rifles);
							}
						}
						else if (Path.GetExtension(path) == ".json")
						{
							string json = JsonConvert.SerializeObject(rifles);
							File.WriteAllText(path, json);
						}
						else if (Path.GetExtension(path) == ".txt")
						{
							string content = $"{rifles[0].name}\n{rifles[0].caliber}\n{rifles[0].patrons_count}\n{rifles[1].name}\n{rifles[1].caliber}\n{rifles[1].patrons_count}";
							File.WriteAllText(path, content);
						}
						break;
					}
					else if(key.Key == ConsoleKey.Escape)
					{
						Console.WriteLine("Выход из программы");
						break;
					}
					
				}
				Console.Clear();
			}

				}


			}
			else
			{
				if (Path.GetExtension(path) == ".xml")
				{
					XmlSerializer xml = new XmlSerializer(typeof(List<Rifle>));
					using (FileStream fs =  new FileStream(path, FileMode.OpenOrCreate))
					{
						xml.Serialize(fs, rifles);
					}
				}
				else if (Path.GetExtension(path) == ".json")
				{
					string json = JsonConvert.SerializeObject(rifles);
					File.WriteAllText(path, json);
				}
				else if (Path.GetExtension(path) == ".txt")
				{
					string content = $"{rifles[0].name}\n{rifles[0].caliber}\n{rifles[0].patrons_count}";
					File.WriteAllText(path, content);
				}
			}
		}
		
	
		public static void Main()
		{
			init();
		}		
		
	}

}
