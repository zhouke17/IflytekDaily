#include "mainwindow.h"
#include <QFileDialog>
#include <QFile>
#include <QTextStream>
#include <QPrintDialog>
#include <QPrinter>
#include <QPainter>
#include <QMatrix>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
{
    setWindowTitle(tr("Eash Word"));
    showWidget = new ShowWidget(this);
    setCentralWidget(showWidget);
    /* 创建动作，菜单，工具栏函数*/
    createActions();
    createMenus();
    createToolBars();
    if(img.load("Images/image.png"))
    {
        showWidget -> imageLabel -> setPixmap(QPixmap::fromImage(img));
    }
}

//创建动作
void MainWindow::createActions()
{
    openFileAction = new QAction(QIcon("Images/打开文件.png"),tr("打开"),this);
    openFileAction -> setShortcut(tr("Ctrl+O"));
    openFileAction -> setStatusTip(tr("打开一个文件"));
    connect(openFileAction,SIGNAL(triggered()),this,SLOT(ShowOpenFile()));

    NewFileAction = new QAction(QIcon("Images/新建文件.png"),tr("新建"),this);
    NewFileAction -> setShortcut(tr("Ctrl+N"));
    NewFileAction -> setStatusTip(tr("新建一个文件"));
    connect(NewFileAction,SIGNAL(triggered()),this,SLOT(ShowNewFile()));

    exitAction = new QAction(QIcon("Images/退出.png"),tr("退出"),this);
    exitAction -> setShortcut(tr("Ctrl+Q"));
    exitAction -> setStatusTip(tr("退出程序"));
    connect(exitAction,SIGNAL(triggered()),this,SLOT(close()));

    copyAction = new QAction(QIcon("Images/复制.png"),tr("复制"),this);
    copyAction -> setShortcut(tr("Ctrl+C"));
    copyAction -> setStatusTip(tr("复制文件"));
    connect(copyAction,SIGNAL(triggered()),this,SLOT(copy()));

    cutAction = new QAction(QIcon("Images/剪切.png"),tr("剪切"),this);
    cutAction -> setShortcut(tr("Ctrl+N"));
    cutAction -> setStatusTip(tr("剪切文件"));
     connect(cutAction,SIGNAL(triggered()),this,SLOT(cut()));

    pasteAction = new QAction(QIcon("Images/粘贴.png"),tr("粘贴"),this);
    pasteAction -> setShortcut(tr("Ctrl+V"));
    pasteAction -> setStatusTip(tr("粘贴文件"));
     connect(pasteAction,SIGNAL(triggered()),this,SLOT(paste()));

    aboutAction = new QAction(QIcon("Images/关于.png"),tr("关于"),this);
    connect(copyAction,SIGNAL(triggered()),this,SLOT(Qapplication::aboutQt()));


    PrintTextAction = new QAction(QIcon("Images/打印文本.png"),tr("打印文本"),this);
    PrintTextAction -> setStatusTip(tr("打印一个文本"));
    connect(PrintTextAction,SIGNAL(triggered()),this,SLOT(ShowPrintText()));

    PrintImageAction = new QAction(QIcon("Images/打印图片.png"),tr("打印图像"),this);
    PrintImageAction -> setStatusTip(tr("打印一幅图像"));
    connect(PrintImageAction,SIGNAL(triggered()),this,SLOT(ShowPrintImage()));

    zoomInAction = new QAction(QIcon("Images/放大.png"),tr("放大"),this);
    zoomInAction -> setStatusTip(tr("放大一张图片"));
    connect(zoomInAction,SIGNAL(triggered()),this,SLOT(ShowZoomIn()));

    zoomOutAction = new QAction(QIcon("Images/缩小.png"),tr("缩小"),this);
    zoomOutAction -> setStatusTip(tr("缩小一张图片"));
        connect(zoomOutAction,SIGNAL(triggered()),this,SLOT(ShowZoomOut()));

    rotate90Action = new QAction(QIcon("Images/旋转90.png"),tr("旋转90°"),this);;
    rotate90Action -> setStatusTip(tr("将一幅图旋转90°"));
    connect(rotate90Action,SIGNAL(triggered()),this,SLOT(ShowRotate90()));

    rotate180Action = new QAction(QIcon("Images/旋转180.png"),tr("旋转180°"),this);;
    rotate180Action -> setStatusTip(tr("将一幅图旋转180°"));
    connect(rotate180Action,SIGNAL(triggered()),this,SLOT(ShowRotate180()));

    rotate270Action = new QAction(QIcon("Images/旋转270.png"),tr("旋转270°"),this);;
    rotate270Action -> setStatusTip(tr("将一幅图旋转270°"));
    connect(rotate270Action,SIGNAL(triggered()),this,SLOT(ShowRotate270()));

    mirrorVerticalAction = new QAction(QIcon("Images/纵向镜像.png"),tr("纵向镜像"),this);
    mirrorVerticalAction -> setStatusTip(tr("对一幅图左纵向镜像"));
connect(mirrorVerticalAction,SIGNAL(triggered()),this,SLOT(ShowMirrorVertical()));

    mirrorHorizontalAction = new QAction(QIcon("Images/横向镜像.png"),tr("横向镜像"),this);
    mirrorHorizontalAction -> setStatusTip(tr("对一幅图做横向镜像"));
    connect(mirrorHorizontalAction,SIGNAL(triggered()),this,SLOT(ShowMirrorHorizontal()));

    undoAction = new QAction(QIcon("Images/撤销.png"),tr("撤销"),this);
    connect(undoAction,SIGNAL(triggered()),this,SLOT(undo()));

    redoAction = new QAction(QIcon("Images/重做.png"),tr("重做"),this);
    connect(redoAction,SIGNAL(triggered()),this,SLOT(redo()));


}

//创建菜单栏
void MainWindow::createMenus()
{
    //文件菜单
    fileMenu = menuBar()->addMenu(tr("文件"));//创建菜单
    fileMenu->addAction(openFileAction);//菜单添加菜单条目(动作)
    fileMenu->addAction(NewFileAction);
    fileMenu->addAction(PrintTextAction);
    fileMenu->addAction(PrintImageAction);
    fileMenu->addSeparator();
    fileMenu->addAction(exitAction);

    //缩放菜单
    zoomMenu = menuBar() -> addMenu(tr("编辑"));
    zoomMenu->addAction(copyAction);
    zoomMenu->addAction(cutAction);
    zoomMenu->addAction(pasteAction);
    zoomMenu->addAction(aboutAction);
    zoomMenu->addSeparator();
    zoomMenu->addAction(zoomInAction);
    zoomMenu->addAction(zoomOutAction);

    //旋转菜单
    roteMenu = menuBar() -> addMenu(tr("旋转"));
    roteMenu->addAction(rotate90Action);
    roteMenu->addAction(rotate180Action);
    roteMenu->addAction(rotate270Action);

    //镜像菜单
    mirrorMenu = menuBar() -> addMenu(tr("镜像"));
    mirrorMenu->addAction(mirrorVerticalAction);
    mirrorMenu->addAction(mirrorHorizontalAction);
}

//创建工具栏
void MainWindow::createToolBars()
{
    //文件工具条
    fileTool = addToolBar("File");
    fileTool->addAction(openFileAction);//工具条添加菜单条目(动作)
    fileTool->addAction(NewFileAction);
    fileTool->addAction(PrintTextAction);
    fileTool->addAction(PrintImageAction);

    //编辑工具条
    zoomTool = addToolBar("Edit");
    zoomTool->addAction(copyAction);
    zoomTool->addAction(cutAction);
    zoomTool->addAction(pasteAction);
    zoomTool->addAction(aboutAction);
    zoomTool->addSeparator();
    zoomTool->addAction(zoomInAction);
    zoomTool->addAction(zoomOutAction);

    //旋转工具条
    rotateTool = addToolBar(tr("rotate"));
    rotateTool->addAction(rotate90Action);
    rotateTool->addAction(rotate180Action);
    rotateTool->addAction(rotate270Action);

    //镜像工具条
    mirrorTool = addToolBar(tr("mirror"));
    mirrorTool->addAction(mirrorVerticalAction);
    mirrorTool->addAction(mirrorHorizontalAction);

    //撤销、重做工具条
    doToolBar = addToolBar(tr("doEdit"));
    doToolBar -> addAction(undoAction);
    doToolBar -> addAction(redoAction);
}

//打开文件
void MainWindow::ShowOpenFile()
{
    filename = QFileDialog::getOpenFileName(this);
    if(!filename.isEmpty())
    {
        if(showWidget->text->document()->isEmpty())
        {
            loadFile(filename);
        }
        else
        {
            MainWindow *newMainWindow = new MainWindow;
            newMainWindow->show();
            newMainWindow->loadFile(filename);
        }
    }
}

//读取文件内容
void MainWindow::loadFile(QString filename)
{
    printf("file name:%s\n",filename.data());
    QFile file(filename);
    if(file.open(QIODevice::ReadOnly|QIODevice::Text))
    {
        QTextStream textstream(&file);
        while(!textstream.atEnd())
        {
            showWidget->text->append(textstream.readLine());
            printf("read line\n");
        }
        printf("end\n");
    }

}

//打开新窗体
void MainWindow::ShowNewFile()
{
    MainWindow *newMainWindow = new MainWindow;
    newMainWindow -> show();
}

//打印文本
void MainWindow::ShowPrintText()
{
    //Qt5中将QPrinter、QPrintDialog等类归入到了printsupport模块中。
    //需要在工程文件（“.pro”文件）中加入“QT+=printsupport”，否则编译会出错。
    QPrinter printer;//新建QPrinter对象
    QPrintDialog printerDlg(&printer,this);
    if(printerDlg.exec())//判断打印对话框是否点击“打印”按钮
    {
        QTextDocument *doc = showWidget->text->document();
        doc->print(&printer);
    }
}

//打印图片
void MainWindow::ShowPrintImage()
{
    QPrinter printer;//新建QPrinter对象
    QPrintDialog printerDlg(&printer,this);
    if(printerDlg.exec())//判断打印对话框是否点击“打印”按钮
    {
        QPainter painter(&printer);
        QRect rect = painter.viewport();//获取QPrinter的视图矩形区域
        QSize size = img.size();//获取图像大小
        size.scale(rect.size(),Qt::KeepAspectRatio);//按照图像比例对矩形进行重置
        painter.setViewport(rect.x(),rect.y(),size.width(),size.height());
        painter.setWindow(img.rect());//设置QPainter窗口大小为图像大小
        painter.drawImage(0,0,img);
        }

}

//放大
void MainWindow::ShowZoomIn()
{
    if(!img.isNull())
    {
        QMatrix matrix;
        matrix.scale(2,2);//横纵2倍放大
        img = img.transformed(matrix);//将图形按照矩阵进行转换
        showWidget->imageLabel->setPixmap(QPixmap::fromImage(img));
    }
}

//缩小
void MainWindow::ShowZoomOut()
{
    if(!img.isNull())
    {
        QMatrix matrix;
        matrix.scale(0.5,0.5);//横纵2倍缩小
        img = img.transformed(matrix);//将图形按照矩阵进行转换
        showWidget->imageLabel->setPixmap(QPixmap::fromImage(img));
    }
}


//旋转
void MainWindow::ShowRotate90()
{
    if(!img.isNull())
    {
        QMatrix matrix;
        matrix.rotate(90);
        img= img.transformed(matrix);
         showWidget->imageLabel->setPixmap(QPixmap::fromImage(img));
    }
}
void MainWindow::ShowRotate180()
{
    if(!img.isNull())
    {
        QMatrix matrix;
        matrix.rotate(180);
        img= img.transformed(matrix);
        showWidget->imageLabel->setPixmap(QPixmap::fromImage(img));
    }
}
void MainWindow::ShowRotate270()
{
    if(!img.isNull())
    {
        QMatrix matrix;
        matrix.rotate(270);
        img= img.transformed(matrix);
        showWidget->imageLabel->setPixmap(QPixmap::fromImage(img));
    }
}

//镜像
void MainWindow::ShowMirrorVertical()
{
    if(!img.isNull())
    {
        img= img.mirrored(false,true);
        showWidget->imageLabel->setPixmap(QPixmap::fromImage(img));
    }
}
void MainWindow::ShowMirrorHorizontal()
{
    if(!img.isNull())
    {
        img= img.mirrored(true,false);
        showWidget->imageLabel->setPixmap(QPixmap::fromImage(img));
    }
}

MainWindow::~MainWindow()
{

}
