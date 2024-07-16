#include "mainwindow.h"
#include <QFileDialog>
#include <QString>
#include <QMenu>
#include <QAction>
MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
{
    setWindowTitle(tr("SVG Viewer"));
    createMenu();
}
void MainWindow::createMenu()
{
    QMenu *menu = menuBar()->addMenu(tr("文件"));
    QAction *openAction = new QAction (tr("打开"),this);
    connect(openAction,SIGNAL(triggered()),this,SLOT(OpenFile()));
    menu->addAction(openAction);
}


void MainWindow::OpenFile()
{
    QString name = QFileDialog::getOpenFileName(this,"打开","/","svg file(*.svg)");
    svgWindow->setFile(name);
}
MainWindow::~MainWindow()
{

}
