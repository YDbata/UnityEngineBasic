using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{

    internal class MyDynamicArray<T> : IEnumerable<T>
        where T : IEquatable<T>
    {
        public int capacity => _items.Length;
        public int Count => _count;


        // 나중에 object type으로도 만들어 보기
        private const int DEFAULT_SIZE = 1;
        private int _count;
        private T[] _items = new T[DEFAULT_SIZE];

        public T this[int index]
        {
            get
            {
                if(index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                return _items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                _items[index] = value;
            }
        }

        // 매치조건 탐색
        public T Find(Predicate<T> match)
        {
            
            for (int i = 0; i < _count; i++)
            {
                if (match.Invoke(_items[i]))
                    return _items[i];
            }

            // int면 0, 참조type이면 null ==> default > 모든 bit가 0반환
            return default;
        }

        public int FindIndex(Predicate<T> match)
        {

            for (int i = 0; i < _count; i++)
            {
                if (match.Invoke(_items[i]))
                    return i;
            }

            // int면 0, 참조type이면 null ==> default > 모든 bit가 0반환
            return -1;
        }

        public MyDynamicArray<T> Add(T _item)
        {
            if(_count >= capacity)
            {
                T[] tmp = new T[_count * 2];
                Array.Copy(_items, tmp, _items.Length);
                _items = tmp;
            }
            _items[_count++] = _item;
            
            return this;
        }

        public void RemoveAt(int index)
        {
            if(index > _count || index < 0) throw new IndexOutOfRangeException();

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
        }

        public bool Remove(T item)
        {
            int index = FindIndex(x => x.Equals(item));
            if (index == -1) return false;

            RemoveAt(index);
            return true;

        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            // 현재 페이지 내용 아래 Current가 두개 있는 이유 공부하기
            public T Current => _data[_index];
            object IEnumerator.Current => _data[_index];

            private readonly MyDynamicArray<T> _data;
            private int _index; // 현재 페이지

            public Enumerator(MyDynamicArray<T> data)
            {
                _data = data;
                _index = -1;
            }


            // 다음 페이지 가져오기
            public bool MoveNext()
            {
                if (_index < _data.Count)
                {
                    _index++;
                    return true;
                }
                return false;
            }

            // 책 표지 덮기
            public void Reset()
            {
                _index = -1;
            }

            // 관리되지 않는 영역의 리소스 해제(책 읽을때 필요한 리소스를 해제하는 함수)
            public void Dispose()
            {
                
            }
        }

    }
}
