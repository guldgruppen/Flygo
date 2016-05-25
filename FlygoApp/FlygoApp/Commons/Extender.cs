using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlygoApp.Commons
{
    public static class Extender
    {
        //Brugestil at convertere en liste af typen IEnumerable til observable collection
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();
            foreach (T item in source)
            {
                collection.Add(item);
            }
            return collection;
        }

    }
}
