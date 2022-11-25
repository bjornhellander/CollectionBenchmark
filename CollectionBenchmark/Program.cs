using System.Diagnostics;

namespace CollectionBenchmark
{
    internal class Program
    {
        static void Main()
        {
            var type = "string";
            var collection_size = 10;
            var iterations = 1000000;

            Console.WriteLine($"Benchmarking {type} with size {collection_size}:");
            Console.WriteLine();

            if (type == "int")
            {
                var list = CreateList(collection_size);
                var keys = list.Select(x => x.Int).ToArray();

                ShowExecutionTime("List (LINQ)", key => SearchForIntInListUsingLinq(list, key), iterations, keys);

                ShowExecutionTime("List (foreach)", key => SearchForIntInListUsingForeach(list, key), iterations, keys);

                ShowExecutionTime("List (for)", key => SearchForIntInListUsingFor(list, key), iterations, keys);

                var dict = list.ToDictionary(x => x.Int);
                ShowExecutionTime("Dictionary", key => SearchForIntInDictionary(dict, key), iterations, keys);
            }
            else if (type == "string")
            {
                var list = CreateList(collection_size);
                var keys = list.Select(x => x.String).ToArray();

                ShowExecutionTime("List (LINQ)", key => SearchForStringInListUsingLinq(list, key), iterations, keys);

                ShowExecutionTime("List (foreach)", key => SearchForStringInListUsingForeach(list, key), iterations, keys);

                ShowExecutionTime("List (for)", key => SearchForStringInListUsingFor(list, key), iterations, keys);

                var dict = list.ToDictionary(x => x.String);
                ShowExecutionTime("Dictionary", key => SearchForStringInDictionary(dict, key), iterations, keys);
            }
            else if (type == "Guid")
            {
                var list = CreateList(collection_size);
                var keys = list.Select(x => x.Guid).ToArray();

                ShowExecutionTime("List (LINQ)", key => SearchForGuidInListUsingLinq(list, key), iterations, keys);

                ShowExecutionTime("List (foreach)", key => SearchForGuidInListUsingForeach(list, key), iterations, keys);

                ShowExecutionTime("List (for)", key => SearchForGuidInListUsingFor(list, key), iterations, keys);

                var dict = list.ToDictionary(x => x.Guid);
                ShowExecutionTime("Dictionary", key => SearchForGuidInDictionary(dict, key), iterations, keys);
            }
        }

        private static List<MyObject> CreateList(int size)
        {
            var obj = new List<MyObject>();
            for (var i = 0; i < size; i++)
            {
                var guid = Guid.NewGuid();
                obj.Add(new MyObject(i, guid.ToString(), guid));
            }
            return obj;
        }

        private static void SearchForIntInListUsingLinq(List<MyObject> obj, int key)
        {
            if (obj.Any(x => x.Int == key))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        private static void SearchForStringInListUsingLinq(List<MyObject> obj, string key)
        {
            if (obj.Any(x => x.String == key))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        private static void SearchForGuidInListUsingLinq(List<MyObject> obj, Guid key)
        {
            if (obj.Any(x => x.Guid == key))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        private static void SearchForIntInListUsingForeach(List<MyObject> obj, int key)
        {
            foreach (var x in obj)
            {
                if (x.Int == key)
                {
                    return;
                }
            }

            throw new InvalidOperationException();
        }

        private static void SearchForStringInListUsingForeach(List<MyObject> obj, string key)
        {
            foreach (var x in obj)
            {
                if (x.String == key)
                {
                    return;
                }
            }

            throw new InvalidOperationException();
        }

        private static void SearchForGuidInListUsingForeach(List<MyObject> obj, Guid key)
        {
            foreach (var x in obj)
            {
                if (x.Guid == key)
                {
                    return;
                }
            }

            throw new InvalidOperationException();
        }

        private static void SearchForIntInListUsingFor(List<MyObject> obj, int key)
        {
            for (var k = 0; k < obj.Count; k++)
            {
                if (obj[k].Int == key)
                {
                    return;
                }
            }

            throw new InvalidOperationException();
        }

        private static void SearchForStringInListUsingFor(List<MyObject> obj, string key)
        {
            for (var k = 0; k < obj.Count; k++)
            {
                if (obj[k].String == key)
                {
                    return;
                }
            }

            throw new InvalidOperationException();
        }

        private static void SearchForGuidInListUsingFor(List<MyObject> obj, Guid key)
        {
            for (var k = 0; k < obj.Count; k++)
            {
                if (obj[k].Guid == key)
                {
                    return;
                }
            }

            throw new InvalidOperationException();
        }

        private static void SearchForIntInDictionary(Dictionary<int, MyObject> obj, int key)
        {
            if (obj.ContainsKey(key))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        private static void SearchForStringInDictionary(Dictionary<string, MyObject> obj, string key)
        {
            if (obj.ContainsKey(key))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        private static void SearchForGuidInDictionary(Dictionary<Guid, MyObject> obj, Guid key)
        {
            if (obj.ContainsKey(key))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        private static void ShowExecutionTime<T>(string title, Action<T> action, int iterations, T[] keys)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var iter = 0; iter < iterations; iter++)
            {
                foreach (var key in keys)
                {
                    action(key);
                }
            }
            Console.WriteLine($"{title}: {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    internal class MyObject
    {
        public MyObject(int i, string s, Guid g)
        {
            Int = i;
            String = s;
            Guid = g;
        }

        public int Int { get; }

        public string String { get; }

        public Guid Guid { get; }
    }
}
