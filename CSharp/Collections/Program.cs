namespace Collections
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            int[] arr = new int[3];
            arr[1] = 1;

            MyDynamicArray<int> da = new MyDynamicArray<int>();
            da[1] = 1;

            da.Find(x => x > 3);

            // using구문 : IDisposable객체의 Dispose()호출을 보장하는 구문
            using (IEnumerator<int> e = da.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    Console.WriteLine(e.Current);
                }

                e.Reset();
            }
            //e.Dispose(); // 얘를 안쓰면 어쩌다 문제가 될 수 있음 때문에 꼭 해는게 좋다. 때문에 using을 사용한다.

            // foreach는 위에 있는 내용을 포함하는 구문으로 IEnumerator를 필요로 한다.
            foreach (var item in da)
            {
                Console.WriteLine(item);
            }
        }
    }
}