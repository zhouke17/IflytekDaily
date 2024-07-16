#include <QCoreApplication>
#include <QFile>
#include <QtDebug>
#include <QDate>
#include <QDataStream>
#include <QDir>
#include <QStringList>
#include <QFileSystemWatcher>
qint64 openFile(const QString &path)
{
    QDir dir(path);
    qint64 size = 0;
    foreach(QFileInfo fileInfo,dir.entryInfoList(QDir::Files))//获取当前目录中文件的大小总和
    {
        size += fileInfo.size();
    }
    foreach(QString subDir,dir.entryList(QDir::Dirs|QDir::NoDotAndDotDot))
    {
        size += openFile(path + QDir::separator() + subDir);//首先上方获取到了目录子文件的大小总和，然后递归打印子目录中文件的大小，最后打印目录总大小。
    }

    char unit = 'B';
    qint64 curSize = size;
    if(curSize > 1024)
    {
        curSize /= 1024;
        unit = 'K';
        if(curSize > 1024)
        {
            curSize /= 1024;
            unit = 'M';
            if(curSize > 1024)
            {
                curSize /= 1024;
                unit = 'G';
            }
        }
    }
    qDebug() << curSize << unit << "\t" << qPrintable(path) << endl;
    return size;
}
int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    //QFile file("text.txt");

    // QFile
    /*if(file.open(QIODevice::ReadOnly))
        {
            char buffer[2048];
            qint64 len = file.readLine(buffer,sizeof(buffer));

    if(len != -1)
    {
        qDebug() << buffer;
    }
}


QTextStream
    if(file.open(QFile::WriteOnly|QFile::Truncate))//将文件内容清空
{
    QTextStream out(&file);
    out << QObject::tr("content:") << qSetFieldWidth(10) << left << 90 << endl;//写入占用10个字符并设置左对齐的内容
}
*/


    //QDataStream
    //将二进制数据写入数据流
    /*    QFile file("binary.dat");
    file.open(QIODevice::WriteOnly|QIODevice::Truncate);
    QDataStream out(&file);
    out << QString("周珂");
    out << QDate::fromString("1993/09/05","yyyy/MM/dd");
    out << (qint32)19;
    file.close();

    //从文件中读取数据
    file.setFileName("binary.dat");
if(!file.open(QIODevice::ReadOnly))
{
    qDebug() << "error!";
}
QDataStream in(&file);
QString name;
QDate birthday;
qint32 age;
in >> name >> birthday >> age;
qDebug() << name << birthday << age;
file.close();
*/


    //文件大小及路径获取
    QStringList args = a.arguments();
    QString path;
    if(args.count() > 1)
    {
        path = args[1];
    }
    else
    {
        path = QDir::currentPath();
    }
    qDebug() << path << endl;
        openFile(path);//递归获取文件大小

    return a.exec();
}


