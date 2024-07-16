#include "satckwindow.h"
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    SatckWindow w;
    w.show();

    return a.exec();
}
