#include "splitter.h"
#include <QApplication>
#include <QSplitter>
#include <QTextEdit>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    QFont font("ZYSong18030",14);
    a.setFont(font);

    //分割主窗口
    QSplitter *splitterMain = new QSplitter(Qt::Horizontal);
    QTextEdit *textLeft = new QTextEdit(QObject::tr("Left Widget1"),splitterMain);
    textLeft -> setAlignment(Qt::AlignCenter);
    QTextEdit *textLeft2 = new QTextEdit(QObject::tr("Left Widget2"),splitterMain);
    textLeft2 -> setAlignment(Qt::AlignCenter);
    QTextEdit *textLeft3 = new QTextEdit(QObject::tr("Left Widget3"),splitterMain);
    textLeft3 -> setAlignment(Qt::AlignCenter);

    // 将第一个区域设置为可折叠的
    splitterMain->setCollapsible(0, true);
    splitterMain->setCollapsible(1, false);
    splitterMain->setCollapsible(2, false);

    //右侧分割
    QSplitter *splitterRight = new QSplitter(Qt::Vertical,splitterMain);
    splitterRight -> setOpaqueResize(true);//设置分割条是否实时显示
    QTextEdit *textUp = new QTextEdit(QObject::tr("Top Widget"),splitterRight);
    textUp -> setAlignment(Qt::AlignCenter);
    QTextEdit *textMiddle = new QTextEdit(QObject::tr("Middle Widget"),splitterRight);
    textMiddle -> setAlignment(Qt::AlignCenter);
    QTextEdit *textBottom = new QTextEdit(QObject::tr("Bottom Widget"),splitterRight);
    textBottom -> setAlignment(Qt::AlignCenter);

    //向右侧增加分割
    QSplitter *splitterLeft = new QSplitter(Qt::Vertical,splitterMain);
    QTextEdit *textLeftUp = new QTextEdit(QObject::tr("Right Widget"),splitterLeft);
    textLeftUp -> setAlignment(Qt::AlignCenter);

    splitterMain -> setStretchFactor(0,1);
    splitterMain -> setStretchFactor(1,3);//设置拉伸比例
    splitterMain -> setStretchFactor(2,1);
    splitterMain -> setWindowTitle(QObject::tr("Splitter"));
    splitterMain -> show();


    //    Splitter w;
    //    w.show();

    return a.exec();
}
