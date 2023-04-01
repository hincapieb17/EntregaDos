using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Path = System.IO.Path;

namespace entregaDos
{
    public class User
    {
        public User() { }

        [PrimaryKey, AutoIncrement]
        [MaxLength(10)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Auxiliar
    {
        static object locker = new object();
        SQLiteConnection conexion;
        public Auxiliar()

        {
            conexion = ConectarBD();
            conexion.CreateTable<User>();
        }

        public SQLite.SQLiteConnection ConectarBD()
        {
            SQLiteConnection conexionBaseDatos;
            string nombreArchivo = "thebrhin.db3";
            string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completaRuta = Path.Combine(ruta, nombreArchivo);
            conexionBaseDatos = new SQLiteConnection(completaRuta);
            return conexionBaseDatos;
        }

        //Guardar - Actualizar
        public int Save(User registro)
        {
            lock (locker)
            {
                if (registro.Id == 0)
                {
                    return conexion.Insert(registro);
                }
                else
                {
                    return conexion.Update(registro);
                }
            }
        }


        //Selecionar 1 registro
        public User SelecionarUno(string User, string pass)
        {
            lock (locker)
            {
                return conexion.Table<User>().FirstOrDefault(x => x.UserName == User  && x.Password == pass);
            }
        }

        //Selecionar Muchos
        public IEnumerable<User> SeleccionarTodo()
        {
            lock (locker)
            {
                return (from i in conexion.Table<User>() select i).ToList();
            }
        }


    }


}