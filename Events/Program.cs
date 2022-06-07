using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    internal class Program
    {
        internal static MailManager mailManager;
        static void Main(string[] args)
        {
            mailManager = new MailManager();
            Fax fax=new Fax();
            Alisa alisa = new Alisa();
            mailManager.SimulateNewMail("Kostretsov Vitaliy <kostr.vit@mail.com", "Pukilson Gina <ginapuk@gmail.com", "Hello from sunny Russia!");
            fax.Unregister();
            mailManager.SimulateNewMail("Perepelkin Maxim <max.perepel@mail.com", "Nicky Alisom <alisnick@gmail.com", "Hello from dimmy Holland!");
            Console.ReadKey();
        }
    }
    // 1. Тип для хранения передаваемой информации
    //    Наследуется от типа System.EventArgs
    //    Тип EventArgs определяется в библиотеке классов NET Framework Class Library (FCL)
    //    и выглядит примерно следующим образом:
    //    [ComVisible(true), Serializable]
    //    public class EventArgs
    //    {
    //        public static readonly EventArgs Empty = new EventArgs();
    //        public EventArgs() { }
    //    }
    internal sealed class NewMailEventArgs : EventArgs
    {
        private readonly string m_from, m_to, m_subject;
        public NewMailEventArgs(string from, string to, string subject) {
            m_from = from; m_to = to; m_subject = subject;
        }
        public string From { get { return m_from; } }
        public string To { get { return m_to; } }
        public string Subject { get { return m_subject; } }
    }

    internal class MailManager
    {
        // Событие определяется в вызывающем типе
        // Такой тип (экземпляр типа) может уведомлять другие объекты (подписчики) о некоторых особых ситуациях
        // События - члены типа, обеспечивающие такое взаимодействие

        // 2. Определение члена-события
        //    Каждому члену-событию определяется:
        //      - область действия (практически всегда открытый)
        //      - тип делегата, указывающий на прототип вызываемого метода (методов)
        //      - имя
        public event EventHandler<NewMailEventArgs> NewMail;
        // Это означает, что  получатели уведомления о событии должны предоставлять метод обратного вызова,
        // прототип которого соответствует типу-делегату EventHandler<NewMailEventArgs>.
        // Так как обобщенный делегат System.EventHandler определен следующим образом:
        // public delegate void EventHandler<TEventArgs>(Object sender, TEventArgs e) where TEventArgs : EventArgs;
        // Поэтому прототип метода должен выглядеть так:
        // void MethodName(Object sender, NewMailEventArgs e);

        // 3. Определение метода, ответственного за уведомление зарегистрированных
        //    объектов о событии.
        //    В соответствии с соглашениями в классе должен быть виртуальный защищенный метод, 
        //    вызываемый из класса и его потомков при возникновении события
        //    Если этот класс изолированный, нужно сделать метод закрытым или невиртуальным
        protected virtual void OnNewMail(NewMailEventArgs e) {
            // копирование 
            EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
            if (temp != null) temp(this, e);
        }

        // 4. Определение метода, преобразующего входную информацию в желаемое событие
        public void SimulateNewMail(string from, string to, string subj)
        {
            // Объект для хранения/передачи информации получателем события/уведомления
            NewMailEventArgs e = new NewMailEventArgs(from, to, subj);

            // Вызвать виртуальный метод , уведомляющий объект о событии
            // Если ни один из производных типов не переопределит этот метод, 
            // объект уведомит всех зарегистрированных получателей уведомления
            OnNewMail(e);
        }
    }

    internal sealed class Fax
    {
        // Передаем конструктору объект MailManager
        public Fax()
        {
            // Создаем экземпляр делегата EventHandler<NewMailEventArgs>, ссылающийся на
            // метод обратного вызова FaxMsg
            // Регистрируем обратный вызов для события NewMail объекта MailManager
            Program.mailManager.NewMail += FaxMsg;
        }

        // MailManager calls this method для уведомления объекта Fax о 
        // прибытии нового почтового сообщения
        private void FaxMsg(object sender, NewMailEventArgs e)
        {
            Console.WriteLine($"Faxing mail message:");
            Console.WriteLine($"------------------------------------------");
            Console.WriteLine($"From: {e.From}");
            Console.WriteLine($"To  : {e.To}");
            Console.WriteLine($"Subj: {e.Subject}");
            Console.WriteLine($"------------------------------------------");
            Console.WriteLine();
        }
        public void Unregister() {
            Program.mailManager.NewMail -= FaxMsg;
        }
    }
    internal sealed class Alisa
    {
        public Alisa()
        {
            Program.mailManager.NewMail += AlisaVoice;
        }

        private void AlisaVoice(object sender, NewMailEventArgs e)
        {
            Console.WriteLine($"Alisa says about new mail:");
            Console.WriteLine($"------------------------------------------");
            Console.WriteLine($"From: {e.From}");
            Console.WriteLine($"To  : {e.To}");
            Console.WriteLine($"Subj: {e.Subject}");
            Console.WriteLine($"------------------------------------------");
            Console.WriteLine();
        }
        public void Unregister()
        {
            Program.mailManager.NewMail -= AlisaVoice;
        }
    }
}
