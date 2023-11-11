using System.Xml.Linq;

namespace Delegates
{
    public class Content
    {
        public string Name;
    }

    public class Youtuber
    {
        private Content[] _contents = new Content[100];
        private int _contentsCount;
        /*public Subscriber[] Subscribers = new Subscriber[100];
        public ContentQA[] contentQAs = new ContentQA[100];
        아래와 같이 추상화 해서 사용이 가능하다.
         */

        // public INotifyContent[] notifyTargets = new INotifyContent[100]; 여기서 한번 더 간단하게 delegate를 이용하였다.

        public delegate void OnContentUploadedHandler(Content content); // 대리자 type : 함수선언과 같이 returntype name(param)형식으로 선언

        // event한정자
        // 외부에서는 이 대리자의 +=과 -=만 사용가능 : 대리자에 사용할 함수를 추가, 삭제하는 기능을 operator +=와 -=로 구현하였다.
        public event OnContentUploadedHandler OnContentUploaded; // 대리자 선언

        public void UploadContent(Content content)
        {
            _contents[_contentsCount++] = content;
            /*for(int i = 0; i < 100; i++) {
                notifyTargets[i].Notification(content);
            }*/
            OnContentUploaded(content);

        }
    }

    public class Manager
    {
        public void OnContentUploaded(Content content)
        {

        }
    }

    public class Subscriber : INotifyContent
    {
        public string name;

        public void Subscribe(Youtuber youtuber)
        {
            youtuber.OnContentUploaded += Notification; // 구독하기 : 대리자의 +=연산 사용
        }

        public void CancelSubscriptionYoutuber(Youtuber youtuber)
        {
            youtuber.OnContentUploaded -= Notification; // 구독취소 : 대리자의 -= 연산 사용
        }
        public void Notification(Content content) {
            Console.WriteLine($"{name}님! {content.Name}이 새로 업로드 되었습니다.");
        }
    }

    public class ContentQA : INotifyContent
    {
        public void Notification(Content content)
        {
            Console.WriteLine($"{content.Name}이 새로 업로드 되었습니다. 검수가 필요합니다.");
        }
    }

    public interface INotifyContent
    {
        void Notification(Content content);
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Youtuber youtuber = new Youtuber();
            Subscriber subscriber1 = new Subscriber();

            subscriber1.Subscribe(youtuber);
        }
    }
}