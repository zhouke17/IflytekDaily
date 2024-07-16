#include <QCoreApplication>
#include <iostream>
#include <fstream>
#include <csignal>
#include <unistd.h>
#include <QDateTime>
#include <QByteArray>
#include <QJsonDocument>
#include <QJsonObject>
#include <QJsonArray>
#include <QMap>
#include<QSet>
#include<QMultiMap>
#include <QProcess>
#include <QThread>


using namespace std;

// 全局变量声明
extern int a1, b1; //定义包含了声明，但是声明不包含定义,此处仅声明
int a3 = 1;


// 函数声明
int func();

// 函数定义
int func()
{
    return a3;
}


//值传递
void change1(int n)
{
    cout<<"value--address: "<<&n<<endl;//显示的是拷贝的地址而不是源地址
    n++;
}
//引用传递
void change2(int & n)
{
    cout<<"refer--address: "<<&n<<endl;
    n++;
}
//指针传递
void change3(int *n)
{
    cout<<"hand--address: "<<n<<endl;
    *n=*n+1;
}


//结构
struct Books
{
    char  title[50];
    char  author[50];
    char  subject[100];
    int   book_id;
};
void printBook( struct Books *book );
// 该函数以结构指针作为参数
void printBook( struct Books *book )
{
    //指针访问结构的成员，您必须使用 -> 运算符
    cout << "book title  : " << (*book).title <<endl;//  ->运算符的实质
    cout << "book author : " << book->author <<endl;
    cout << "book subject : " << book->subject <<endl;
    cout << "book ID : " << book->book_id <<endl;
}


class Base
{
public:
    virtual void func()
    {
        cout<<"This is Base"<<endl;
    }
};
class _Base:public Base
{
public:
    void func() final//正确，func在Base中是虚函数
    {
        cout<<"This is _Base"<<endl;
    }
};
class __Base:public _Base
{
    /*    public://不正确，func在_Base中已经不再是虚函数，不能再被重写
        void func()
        {
            cout<<"This is __Base"<<endl;
        }*/
};


//信号处理器
void signalHandler( int signum )
{
    cout << "Interrupt signal (" << signum << ") received.\n";

    // 清理并关闭
    exit(signum);
}

//字符串比较
void TrueOrFalse (int x)
{
    cout << (x?"True":"False")<<endl;
}

enum Color { Red, Green, Blue };
Color curColor;
void setColor(Color col)
{
    curColor = col;
}
Color getColor()
{
    return curColor;
}


// 使用模板定义一个泛型方法，接受QMap作为参数
template <typename KeyType, typename ValueType>
QString processMap(const QMap<KeyType, ValueType>& map,QString trueValue)
{
    for (auto it = map.constBegin(); it != map.constEnd(); ++it) {
        if(it.value() == trueValue)
        {
            return it.key();
        }
    }
}
int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
#if 1
    //输出字符串
    std::cout << "Hello,World!" << endl;


    //变量定义，初始化(变量只允许定义一次，但可以在多个文件中声明)
    int a1 = 1, b1 = 2;//定义时直接包含了变量的声明
    cout << a1 + b1 << endl;


    //定义枚举类型
    enum color {red,blue,black} c;
    c = blue;
    std::cout << "blue is "<< c << endl;


    //类型转换
    int a2 = 10;
    //float b = static_cast<float>(a2);
    float b = (float)a2;
    cout << b << endl;


    //作用域
    int a3 = 10; //a3此处为局部变量，与全局变量a3互不影响
    {
        a3 = 20;  // 块作用域变量
        std::cout << "block  variable : " << a3 << std::endl;
    }
    std::cout << "extern  variable: " << a3 << std::endl;

    int a4 = func();
    cout << "a4=" << a4 << endl;


    //while循环
    int a5 = 10;
    int count = 0;
    while (a5--) // 任意非零值时都为真;当条件为真时执行循环,当条件为假时，程序流将继续执行紧接着循环的下一条语句。
    {
        ++count;
        printf("%d,a5=%d\n", count, a5);
    }
    printf("last a5=%d\n", a5);


    //基于范围的 for循环
    string str("some string");
    // range for 语句
    for(auto &c : str)
    {
        c = toupper(c);
    }
    cout << str << endl;

    //数组，声明时大小固定
    int my_array[5] = { 1, 2, 3, 4, 5 };
    cout << "my_array length: " << sizeof(my_array) / sizeof(my_array[0])<< endl;


    // 不会改变 my_array 数组中元素的值
    // x 将使用 my_array 数组的副本
    for (int x : my_array)
    {
        x *= 2;
    }
    for (int x : my_array)
    {
        cout << x << endl;
    }


    // 会改变 my_array 数组中元素的值
    // 符号 & 表示 x 是一个引用变量，将使用 my_array 数组的原始数据
    // 引用是已定义的变量的别名
    for (int &x : my_array)
    {
        x *= 2;
    }
    for (int x : my_array)
    {
        cout << x << endl;
    }

    // vector向量用于存储大小可变的动态数组
    vector<int> vec;
    // 显示 vec 初始大小
    cout << "vector size = " << vec.size() << endl;

    // 向向量 vec 追加 5 个整数值
    for(int i = 0; i < 5; i++)
    {
        vec.push_back(i);
    }

    // 显示追加后 vec 的大小
    cout << "extended vector size = " << vec.size() << endl;



    //lambda
    int a6 = 10;

    auto add_a6 = [a6](int a) mutable  {a6 *= 2; return a + a6; };  // 无法修改通过复制形式捕捉的变量,通过nutable改变其const属性

    cout << "add_a6=" << add_a6(10) << endl; // 输出 30


    //string
    string http = "www.runoob.com";
    //打印字符串长度
    cout<< "http length:" << http.length()<<endl;
    //拼接
    http.append("/C++");
    cout<< "http length:" << http<<endl; //打印结果为：www.runoob.com/C++
    //删除
    int pos = http.find("/C++"); //查找"C++"在字符串中的位置
    cout<<pos<<endl;
    http.replace(pos, 4, "");   //从位置pos开始，之后的4个字符替换为空，即删除
    cout<< "http length:" << http <<endl;
    //找子串runoob
    int first = http.find_first_of("."); //从头开始寻找字符'.'的位置
    int last = http.find_last_of(".");   //从尾开始寻找字符'.'的位置
    cout<<http.substr(first+1, last-first-1)<<endl; //提取"runoob"子串并打印
    string s1 = "abc",s2 = "bcd";
    cout << "s1 <= s2: ";
    TrueOrFalse(s1 >= s2);//大于：1(true)，小于:0(false),等于：1
    cout << "s1 <= s2: " << endl;
    int s3 = s1.compare(s2);//大于：1，小于：-1，等于：0
    cout << "s1 compare s2" << s3 << endl;
    QString s4 = "12345";
    cout << "s4: ";
    cout << s4.toInt() << endl;



    //指针
    int  var = 20;   // 实际变量的声明
    int  *ip;        // 指针变量的声明( "ip" 需要赋于一个地址(可以用 & 符号获取其他变量的地址再赋值给 ip)，而 "*ip" 是一个具体的值，即读取地址后获得的值；)

    ip = &var;       // 在指针变量中存储 var 的地址

    cout << "var:";
    cout << var << endl;

    cout << "ip address:";
    cout << ip << endl;

    cout << " *ip :";
    cout << *ip << endl;


    //值传递、引用传递、指针传递
    int n=10;
    cout<<"actual`s address:"<<&n<<endl;
    change1(n);
    cout<<"after change1() n="<<n<<endl;
    change2(n);
    cout<<"after change2() n="<<n<<endl;
    change3(&n);
    cout<<"after change3() n="<<n<<endl;


    //结构体
    Books Book1;        // 定义结构体类型 Books 的变量 Book1
    Books Book2;        // 定义结构体类型 Books 的变量 Book2

    // Book1 详述
    strcpy( Book1.title, "C++ Course");
    strcpy( Book1.author, "Runoob");
    strcpy( Book1.subject, "Program Language");
    Book1.book_id = 12345;

    // Book2 详述
    strcpy( Book2.title, "CSS Course");
    strcpy( Book2.author, "Runoob");
    strcpy( Book2.subject, "Web Technology");
    Book2.book_id = 12346;

    // 通过传 Book1 的地址来输出 Book1 信息
    printBook( &Book1 );

    // 通过传 Book2 的地址来输出 Book2 信息
    printBook( &Book2 );

    //注释：访问结构的成员时使用.点运算符，而通过指针访问结构的成员时，则使用->(*和.的简洁方式)箭头运算符。


    //虚函数（纯虚函数类似与c#抽象函数）
    _Base a7;
    __Base b2;
    Base *ptr = &a7;
    cout << "ptr: " << ptr << endl;
    ptr->func();
    ptr=&b2;
    _Base* ptr2=&b2;
    ptr->func();
    ptr2->func();


    //信号
    /*
    int i= 0;
    // 注册信号 SIGINT 和信号处理程序
    signal(SIGINT, signalHandler);
    while(++i){
        cout << "Going to sleep...." << endl;
        if( i == 3 ){
            raise( SIGINT);//生成信号
        }
        sleep(1);
    }
    */



    //文件流
    //    char data[100];
    //    ofstream outfile;
    //    ifstream infile;
    //    outfile.open("test.txt", ios::out | ios::trunc );
    //    cout << "Writing to the file" << endl;
    //    cout << "Enter your name: ";
    //    cin.getline(data, 100);

    //    // 向文件写入用户输入的数据
    //    outfile << data << endl;

    //    cout << "Enter your Age: ";
    //    cin >> data;
    //    cin.ignore();

    //    // 再次向文件写入用户输入的数据
    //    outfile << data << endl;

    //    // 关闭打开的文件
    //    outfile.close();

    //    // 以读模式打开文件
    //    infile.open("test.txt");

    //    cout << "Reading from the file" << endl;
    //    infile >> data;
    //    // 在屏幕上写入数据
    //    cout << data << endl;

    //    // 再次从文件读取数据，并显示它
    //    infile >> data;
    //    cout << data << endl;

    //    // 关闭打开的文件
    //    infile.close();


    //    //一次读完
    //    infile.open("test.txt");
    //    cout << "Read it all at once:" <<endl;
    //    string result;
    //    while(infile >> data)
    //    {
    //        cout << data << endl;
    //        result = result + data;
    //    }
    //    cout << "result:" << result << endl;


    //QList（java风格迭代器）
    QList<int> qlist;
    QMutableListIterator<int> qIterator (qlist);
    for (int i = 0;i < 10; ++i) {
        qIterator.insert(i);
    }
    cout << "Original qlist: " << endl;
    for (qIterator.toFront();qIterator.hasNext();) {
        cout << qIterator.next();
    }
    for (qIterator.toBack();qIterator.hasPrevious();) {
        if(qIterator.previous() % 2 == 0)
            qIterator.remove();
        else
            qIterator.setValue(qIterator.peekNext() * 10);
        //为何使用peekNext()而不是peekprevious？？？
        //原因有可能是上一个判断导致迭代点移动到上一个元素之前，所以使用peeknext获取下一个元素
    }
    cout << endl << "New qlist: " << endl;
    for (qIterator.toFront();qIterator.hasNext();) {
        cout << qIterator.next();
    }

    cout << endl;

    //STL风格迭代器
    QList<int> list;
    for (int i = 0;i < 10; i++) {
        list.insert(list.end(),i);
    }
    QList<int>::iterator ii;//读写迭代器
    for (ii = list.begin();ii != list.end();ii++) {
        cout << "STL style:" << *ii << endl;
        *ii = *ii * 10;
    }
    cout << "output resutl" << endl;
    QList<int>::const_iterator jj;//常量只读迭代器
    for (jj = list.begin();jj != list.end();jj ++) {
        cout << *jj << endl;
    }

    QDateTime *time = new QDateTime(QDateTime::currentDateTime());
    cout << "Current System Time: " << time->toString("yyyy-MM-dd hh:mm:ss").toStdString();//`std::ostream`的`operator<<`不支持直接输出`QString`类型的对象,故需要使用toStdString()方法：将`QString`对象转换为C风格的字符串。


    cout << endl;

    //&理解
    int aa=10;
    int cc=20;
    int &bb=aa;//将bb的地址指向了aa的地址
    cout <<"aa="<<aa<<endl;
    cout <<"aa adress:"<<&aa<<endl;
    cout <<"bb="<<bb<<endl;
    cout <<"bb adress:"<<&bb<<endl;
    cout <<"cc="<<cc<<endl;
    cout <<"cc adress:"<<&cc<<endl;
    bb=cc; //赋值操作后aa的值也发生改变，说明更改的是地址指向的值
    cout <<"Changed"<<endl;
    cout <<"aa="<<aa<<endl;
    cout <<"aa adress:"<<&aa<<endl;
    cout <<"bb="<<bb<<endl;
    cout <<"bb adress:"<<&bb<<endl;
    cout <<"cc="<<cc<<endl;
    cout <<"cc adress:"<<&cc<<endl;
    aa =22;
    cout << "bb:" << bb <<endl;
    cout << "cc:" << cc <<endl;


    //*理解
    int i=100;
    int* p = &i;
    cout << "&i" << &i << endl;
    cout << "p:" << p << endl;
    cout << "*p:" << *p << endl;
    *p = *p + 1;
    cout << "*p +1:" << *p <<endl;


    //强制类型转换
    //static_cast

    //1.基本类型间转换
    double d=1;
    int i1 =static_cast<int>(d);
    cout << "between base datatype static_cast:" << i1 << endl;

    //2.void指针与基本类型间转换
    int i2 = 0;
    void *p1 = &i2;
    int *p2 = static_cast<int*>(p1);
    *p2 += 3;
    cout << "void * with base datatype static_cast:" << i2 << endl;

    //const_cast
    //将 const 引用转换为同类型的非 const 引用
    const string s ="Inception";
    string &p3 =const_cast<string&>(s);
    cout << "const_cast &: " << p3 << endl;

    //将 const 指针转换为同类型的非 const 指针
    string* p4 = const_cast<string*>(&s);
    cout << "const_cast *: " << *p4 << endl;


    //QByteArray可以存储任意类型数据
    QByteArray byte = "{\"name\":\"zhangsan\",\"age\":22}";

    //QJsonDocument
    QJsonDocument document = QJsonDocument::fromJson(byte);
    if(!document.isNull())
    {
        if(document.isObject())
        {
            QJsonObject object = document.object();
            QString name = object["name"].toString();
            int age = object["age"].toInt();
            cout << "name:" << name.toStdString() << "   age:" << age << endl;

            //转化
            QJsonDocument doc = QJsonDocument(object);
            QString str = QString::fromUtf8(doc.toJson());
            cout << "QJsonObject translate into QJsonDocument: " << str.toStdString();
        }
    }

    QJsonArray jsonArray = {1,2,3,4};
    document = QJsonDocument(jsonArray);
    if(!document.isNull())
    {
        if(document.isArray())
        {
            QJsonArray array = document.array();
            for(const auto &value : array)
            {
                if (value.isDouble()) {
                    double number = value.toDouble();
                    cout << qPrintable(QString("QJsonArray:%1").arg(number)) << endl;
                    // 处理数字类型子元素
                } else if (value.isString()) {
                    QString str = value.toString();
                    cout << qPrintable( QString("QJsonArray:%1").arg(str)) << endl;
                    // 处理字符串类型子元素
                } else if (value.isBool()) {
                    bool flag = value.toBool();
                    cout << qPrintable( QString("QJsonArray:%1").arg(flag)) << endl;
                    // 处理布尔类型子元素
                }
            }
        }
    }

    QString str2 = "Hello,Qt";

    QByteArray byteArray = str2.toUtf8();
    std::cout << "byteArray: " << byteArray.constData() << std::endl;

    QString newStr = QString::fromUtf8(byteArray);
    std::cout << "newStr: " << newStr.toStdString() << std::endl;

    QString newStr2 = byteArray;
    cout << "Strong Convet QByteArray: " << newStr2.toStdString() << endl;


    QMap<QString,int> map;//字典
    map.insert("one",1);
    map.insert("three",2);
    map.insert("three",3);//注意：map类型可以插入重复的键,重复会覆盖前边的元素

    for (const QString &key:map.keys()) {
        std::cout << "QMap  " << key.toStdString()  << ":" << map.value(key) << endl;
    }
    foreach (const QString &key, map.keys()) {
        std::cout << "QMap  " << key.toStdString()  << ":" << map.value(key) << endl;
    }

    if(map.contains("three"))
    {
        map.remove("three");
    }
    cout << "map remaining:" << map.count() << endl;

    QMultiMap<QString, int> multiMap;//注意：multimap可以插入重复键，不会覆盖，遍历时可以选择是否遍历唯一的键

    // 插入元素
    multiMap.insert("apple", 1);
    multiMap.insert("banana", 2);
    multiMap.insert("banana", 3);

    // 查找元素
    foreach (const QString& key, multiMap.uniqueKeys()) {
//        QList<int> values = multiMap.values(key);
//        foreach (const int& x,values)
        {
            std::cout << "QMultiMap unique keys: " << key.toStdString()  << " : " << multiMap.value(key) << endl;
        }
    }
    foreach(auto key,multiMap.keys())
    {
        cout << "QMultiMap all keys: " << key.toStdString() << " : " << multiMap.value(key) << endl;
    }

    QSet<QString> set;
    set.insert("one");
    set.insert("two");
    set.insert("two");

    cout << "QSet count: " << set.count() << endl;

    //ping
    QString network_cmd = "ping www.baidu.com -n 2 -w 500";
    QString result;
    QProcess *network_process = new QProcess();    //不要加this
    while(true)
    {
        network_process->start(network_cmd);   //调用ping 指令
        network_process->waitForFinished();    //等待指令执行完毕
        result = QString::fromLocal8Bit(network_process->readAllStandardOutput());   //获取指令执行结果
        if(result.contains(QString("TTL=")))   //若包含TTL=字符串则认为网络在线
        {
            cout << "online" << endl;  //在线
            break;
        }
        else
        {
            cout << "outline" << endl; //离线
            break;
        }
        sleep(1);  //加sleep降低CPU占用率
    }

    setColor(Color::Blue);
    int type = getColor();
    int type2 = static_cast<int>(Color::Blue);
    Color color = static_cast<Color>(2);
    Color color2 = Color(2);
    cout << "type:" << color <<endl;


    QJsonObject jsonObj;
    jsonObj["name"] = "Alice";
    jsonObj["age"] = 30;

    // 获取属性"name"的当前字符串值，并进行增加操作
    QString currentName = jsonObj["name"].toString();
    currentName.append(" Johnson");
    jsonObj["name"] = jsonObj["name"].toString() +  ",Alice";
    cout << "QJsonObject attribute add:" << jsonObj["name"].toString().toStdString() << endl;


    QString json1 = "你好,我,是,你,爸爸";
    QString json2 = "是";
    int index1 = json1.indexOf(json2);
    if(index1 != -1)
    {
        if(index1 == 0)
        {
            json1.remove(index1,json2.length() + 1);
        }
        else if(index1 > 0 && index1 < json1.length() - 1) {
            json1.remove(index1,json2.length() + 1);
        }
        else
        {
            json1.remove(index1 - 1,json2.length() + 1);
        }
    }
    //    cout << "json1:" << json1.toStdString() << endl;


    QString str1 = "4,3,3333";
    if(str1.indexOf("33,") != -1)
    {
        cout << "yes" << endl;
    }
    else {
        cout << "no" <<endl;
    }

    QStringList list1;//默认为空
    //    if(list1.first().isEmpty()) //报错
    //    {
    //        cout << "list1 first is Empty" << endl;
    //    }
    if(list1.isEmpty() || list1.first().isEmpty())//完美判断组合
    {
        cout << "list1 is Empty" << endl;
    }
    cout << list1.count() << list1.isEmpty() << list1.contains("1") << endl; //0  1  0


    list1.append("");//赋值时可能出现的情况
    cout << list1.count() << endl;//1
    if(list1.isEmpty() || list1.first().isEmpty())//完美判断组合
    {
        cout << "list1 is Empty" << endl;
    }
    list1.append("1");
    cout << list1.count() << endl;//2
    QString str3 = list1.join(",");
    cout << "list1.join , ->" << str3.toStdString() << endl;//，1


    list1.removeAll(QString(""));
    cout << "list1 count:" << list1.count() << endl;//1


    str3 = list1.join(",");
    cout << "list1 join , ->" << str3.toStdString() << endl;//1



    QJsonObject obj;
    obj["one"] = "";
    QStringList list2 = obj["one"].toString().split(",");//赋值了一个空字符
    if(list2.isEmpty())//false
    {
        cout << "list2 is not Empty" << endl;
    }
    else {
        cout << "list2 count:" << list2.count() <<endl;
    }
    if(!list2.contains("1"))//true
    {
        cout << "list2 not contain 1:" << endl;
    }
    list2.removeAll(QString(""));
    cout << "list2 is empty: " << list2.isEmpty() << endl;//1
    cout << "list2 count: " << list2.count() << endl;//0
    cout << "list2 contain 1: " << list2.contains("1") << endl;
    cout << "list2 join ,: " << list2.join(",").toStdString() << endl;

    if(obj["one"].toString().isEmpty())//true
    {
        cout << "obj is Empty" << endl;
    }

    obj["one"] = "1,2,3,4";
    list2 = obj["one"].toString().split(",");
    cout << "list2 has:" << list2.join("-").toStdString() <<endl;//1-2-3-4
    if(list2.contains("1"))//true
    {
        list2.removeOne("1");
    }
    cout << "list2 remain:" << list2.join("-").toStdString() <<endl;//2-3-4



    QMap<QString,QString> map2;
    map2.insert("1","one");
    map2.insert("2","two");
    map2.insert("3","three");
    foreach(auto item,map2.keys())
    {
        cout << "map2: " << map2.value(item).toStdString() << endl;
    }

    foreach(auto item ,map2.keys())
    {
        if(map2.value(item) == "three")
        {
            cout << "three` s key: " << item.toStdString() << endl;
        }
    }

    cout << "processMap: " <<  processMap(map2,"three").toStdString() << endl;

    map2.insertMulti("3","otherThree");
    foreach(auto item,map2.keys())
    {
       cout << "map2 after insertMulti: " << map2.value(item).toStdString() << endl;
    }

#endif
    return a.exec();
}
