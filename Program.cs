using System;

// Пример реализации синглтона для какого-нибудь сервиса (например, для работы с БД)
public class SomeService {
	// Собственно сам экземпляр класса, статическое поле, то есть имеет одинаковое значение на весь класс
	private static SomeService instance;
	
	// Конструктор класса, приватный, чтобы нельзя было инициализировать класс извне
	// Можно сюда записать какую-то логику инициализации (например, настройку того же коннекта к бд), но в данном примере её нет
	private SomeService() {
		
	}
	
	// Метод для получание экземпляра класса. Если он ещё не инициализирован, то делаем это, после чего возвращаем инстанс
	// Это самая простая реализация данного метода. Но у него могут быть проблемы, например, с многопоточностью
	// В таком случае, можно сделать использовать оператор lock на каком-нибудь объекте синхронизации, чтобы несколько потоков одновременно не могли переобъявить экземпляр
	public static SomeService GetInstance() {
		if (instance == null) 
		{
			instance = new SomeService();
		}
		return instance;
	}
	
	/**
	 *  Вот так бы выглядела реализация, решающая проблему с многопоточностью
	 *  В данном случае есть объект syncRoot, который лочится при инициализации экземпляра
	 *  При этом внутри есть ещё одна проверка, так как к моменту блокировки другой поток уже мог инициализировать экзмпляр,
	 *  так называемый double-check locking
	 *
	 *  private static object syncRoot = new Object();
	 *
	 *	public static SomeService GetInstance()
	 *	{
	 *		if(instance == null)
	 *		{
	 *			lock(syncRoot)
	 *			{
	 *				if (instance == null)
	 *				{
	 *					instance = new SomeService();
	 *				}
	 *			}
	 *		}
	 *		return instance;
	 *	}
	 **/
	
	// Просто какой-то метод класса
	
	public String SayHello() {
		return "Hello, World!";
	}
}

class Program {
	static void Main(string[] args) {
		
		// Теперь мы можем получить экземаляр нашего сервиса и вызывать его метод
		SomeService service = SomeService.GetInstance();
		
		Console.Write(service.SayHello());
	}
}