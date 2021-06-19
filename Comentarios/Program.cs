using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Comentarios
{
    public class ComentariosDB
    {

        public static void SaveToFile(List<comentarios> Listacomentario, string path)
        {
            StreamWriter textOut = null; 

            try
            {

                textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));  
                foreach (var c in Listacomentario)
                {
                    textOut.Write(c.id + "|");
                    textOut.Write(c.autor + "|");
                    textOut.Write(c.fecha_publicacion + "|");
                    textOut.Write(c.Comentario + "|");
                    textOut.Write(c.ip + "|");
                    textOut.Write(c.es_inapropiado + "|");
                    textOut.WriteLine(c.Likes);
                }
            }
            catch (IOException e)   
            {
                Console.WriteLine("Ya existe el archivo");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally         
            {
                if (textOut != null)
                {
                    textOut.Close();
                }
            }
        }


        public static List<comentarios> ReadFromFile(string path)
        {
            List<comentarios> comentarios = new List<comentarios>();

            StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));    

            try
            {

                while (textIn.Peek() != -1) 
                {
                    string row = textIn.ReadLine(); 
                    string[] columns = row.Split('|');
                    comentarios c = new comentarios();

                    c.id = int.Parse(columns[0]);
                    c.autor = columns[1];
                    c.fecha_publicacion = DateTime.Parse(columns[2]);
                    c.comentario = columns[3];
                    c.ip = columns[4];
                    c.es_inapropiado = int.Parse(columns[5]);
                    c.Likes = int.Parse(columns[6]);
                    comentarios.Add(c);
                }

            }

            catch (IOException e)
            {
                Console.WriteLine("Ya existe el archivo");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally
            {

                textIn.Close();

            }

            return comentarios;

        }

        public static void GetLikes(string path)
        {
            List<comentarios> comentarios;

            try        
            {

                comentarios = ReadFromFile(path);

                var filter_comentarios = from c in comentarios    
                                         orderby c.Likes descending
                                         select c;

                foreach (var c in filter_comentarios)     
                    if (c.es_inapropiado > 0)          
                    {
                        Console.WriteLine("Este mensaje se ha eliminado por ser inapropiado\n");
                    }
                    else
                    {
                        Console.WriteLine(c);
                    }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void GetFecha(string path)
        {
            List<comentarios> comentarios;

            try
            {

                comentarios = ReadFromFile(path);

                var filter_comentarios = from c in comentarios
                                         orderby c.fecha_publicacion descending
                                         select c;

                foreach (var c in filter_comentarios)
                    if (c.es_inapropiado > 0)
                    {
                        Console.WriteLine("Este mensaje se ha eliminado por ser inapropiado\n");
                    }
                    else
                    {
                        Console.WriteLine(c);
                    }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }



    public class comentarios
    {
        public int id;
        public string autor;
        public DateTime fecha_publicacion;     
        public string comentario;
        public string ip;
        public int es_inapropiado;
        public int likes;
        public override string ToString()       
        {
            return string.Format($"Id: {id} {autor} Fecha: {fecha_publicacion} \n {comentario} ip:{ip} {es_inapropiado} \n Likes: {likes} \n"  );
        }
        public comentarios()
        {
           
        }
        public comentarios(int id, string autor, DateTime fecha_publicacion, string comentario, string ip, int es_inapropiado, int likes)
        {
            Id = id;
            Autor = autor;
            Fecha_publicacion = fecha_publicacion;
            Comentario = comentario;
            Ip = ip;
            Es_inapropiado = es_inapropiado;
            Likes = likes;
        }

        public int Id { get => id; set => id = value; }
        public string Autor { get => autor; set => autor = value; }
        public DateTime Fecha_publicacion { get => fecha_publicacion; set => fecha_publicacion = value; }    
        public string Comentario { get => comentario; set => comentario = value; }
        public string Ip { get => ip; set => ip = value; }
        public int Es_inapropiado { get => es_inapropiado; set => es_inapropiado = value; }
        public int Likes { get => likes; set => likes = value; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string R;        

            List<comentarios> com = new List<comentarios>();

            com.Add(new comentarios(1, "Roberto", new DateTime(2015, 02, 2), "Hace un buen dia", "192.1.45.310", 0, 5));
            com.Add(new comentarios(2, "Juan", new DateTime(2015, 01, 13), "El mejor es el America", "192.1.46.620", 2, 1));
            com.Add(new comentarios(3, "Gabriel", new DateTime(2017, 03, 12), "Arriba las chivas", "192.1.25.610", 0, 50));
            com.Add(new comentarios(4, "Jarim", new DateTime(2021, 05, 20), "Mi cama es mejor la tuya", "192.1.67.310", 0, 17));
            com.Add(new comentarios(5, "Islam", new DateTime(2017, 04, 15), "La existencias de los veganos es innecesaria", "192.1.40.210", 1, 10));
            com.Add(new comentarios(6, "Oliver", new DateTime(2020, 12, 22), "Si la tierra es plana, entonces explica por que la gorda de tu madre no para de rodar", "192.1.15.236", 7, 25));
            com.Add(new comentarios(7, "Marcelo", new DateTime(2018, 07, 19), "Papa Francisco V", "192.1.78.456", 0, 5));
            com.Add(new comentarios(8, "Sonia", new DateTime(2021, 01, 27), "Roblox es el mejor juego", "192.1.35.756", 0, 15));

            ComentariosDB.SaveToFile(com, @"D:\Eliver\Documents\OPP\File\Archivocomentarios.txt");

            ComentariosDB.ReadFromFile(@"D:\Eliver\Documents\OPP\File\Archivocomentarios.txt");

            foreach (var item in com)
            {
                if (item.es_inapropiado>0)
                {   
                    Console.WriteLine("Este mensaje se ha eliminado por ser inapropiado\n");       
                }
                else
                {
                    Console.WriteLine(item);
                }
                
            }

            Console.WriteLine("¿Le gustaria ordenar los comentarios por Likes?");
            Console.WriteLine("s para si, y n para no");

            R = Console.ReadLine();

            if (R == "s")
            {
                Console.Clear();
                ComentariosDB.GetLikes(@"D:\Eliver\Documents\OPP\File\Archivocomentarios.txt");
            }
            else if (R == "n")
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("el caracter introducido no es valido");
            }

            Console.WriteLine("¿Le gustaria ordenar los comentarios por Fecha?");
            Console.WriteLine("s para si, y n para no");

            R = Console.ReadLine();

            if (R == "s")
            {
                Console.Clear();
                ComentariosDB.GetFecha(@"D:\Eliver\Documents\OPP\File\Archivocomentarios.txt");
            }
            else if (R == "n")
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("el caracter introducido no es valido");
            }


            Console.WriteLine("¿Le gustaria agregar un comentario?");
            Console.WriteLine("s para si, y n para no");
            R = Console.ReadLine();

            if (R == "s")
            {
                Console.Clear();
                Console.WriteLine("Escriba el comentario");
                R = Console.ReadLine();
                comentarios comm = new comentarios(9, "Pepe", DateTime.Today, R, "192.1.10.335", 0, 0);    
                com.Add(comm);      
                ComentariosDB.SaveToFile(com, @"D:\Eliver\Documents\OPP\File\Archivocomentarios.txt");
                ComentariosDB.ReadFromFile(@"D:\Eliver\Documents\OPP\File\Archivocomentarios.txt");
                foreach (var item in com)
                {
                    Console.WriteLine(item);
                }
            }
            else if (R == "n")
            {
                
            }
            else
            {
                Console.WriteLine("el caracter introducido no es valido");
            }

        }
    }
}
