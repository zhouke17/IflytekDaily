#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QImage>
#include <QLabel>
#include <QMenu>
#include <QMenuBar>
#include <QAction>
#include <QComboBox>
#include <QSpinBox>
#include <QToolBar>
#include <QFontComboBox>
#include <QToolButton>
#include <QTextCharFormat>
#include "showwidget.h"

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    void createActions(); //创建动作
    void createMenus();//创建菜单
    void createToolBars();//创建工具栏
    void loadFile(QString filename);
    void mergeFormat(QTextCharFormat);
private :
    QMenu *fileMenu;  //各项菜单
    QMenu *zoomMenu;
    QMenu *roteMenu;
    QMenu *mirrorMenu;
    QImage img;
    QString filename;
    ShowWidget *showWidget;
    QAction *openFileAction;//文件菜单项
    QAction *NewFileAction;
    QAction *PrintTextAction;
    QAction *PrintImageAction;
    QAction *exitAction;//编辑菜单项
    QAction *copyAction;
    QAction *cutAction;
    QAction *pasteAction;
    QAction *aboutAction;
    QAction *zoomInAction;
    QAction *zoomOutAction;
    QAction *rotate90Action; //旋转菜单项
    QAction *rotate180Action;
    QAction *rotate270Action;
    QAction *mirrorVerticalAction; //镜像菜单项
    QAction *mirrorHorizontalAction;
    QAction *undoAction;
    QAction *redoAction;
    QToolBar *fileTool; //工具栏
    QToolBar *zoomTool;
    QToolBar *rotateTool;
    QToolBar *mirrorTool;
    QToolBar *doToolBar;

protected slots:
    void ShowNewFile();
    void ShowOpenFile();
    void ShowPrintText();
    void ShowPrintImage();
    void ShowZoomIn();
    void ShowZoomOut();
    void ShowRotate90();
    void ShowRotate180();
    void ShowRotate270();
    void ShowMirrorVertical();
    void ShowMirrorHorizontal();
};

#endif // MAINWINDOW_H
