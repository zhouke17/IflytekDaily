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

signals:
    void mySignal(const QString &msg);

    //void sliderTextChanged(int value);

private slots:
    void mySlot(const QString &msg);
    void mySlot2(int value);

    //void showSlilderLocation(int value);

    void setTextFontColor();

    void quite();

    void on_CheckUnderLine_clicked(bool checked);

    void on_CheckItalic_clicked(bool checked);

    void on_CheckBold_clicked(bool checked);

    void on_myhorizontalSlider_valueChanged(int value);

private:
    Ui::MainWindow *ui;
};

#endif // MAINWINDOW_H
