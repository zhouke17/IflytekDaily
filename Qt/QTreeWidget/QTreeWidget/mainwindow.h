#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:

    void on_Drag_pushButton_clicked();//打开Drag_pushButton窗体

    void on_UpDwon_pushButton_clicked();//打开UpandWodnTreeWidget窗体

    void on_DrapEventFilter_pushButton_clicked();

    void on_OverrideTreeWidget_pushButton_clicked();

    void on_Origin_pushButton_clicked();

private:
    Ui::MainWindow *ui;
};

#endif // MAINWINDOW_H
