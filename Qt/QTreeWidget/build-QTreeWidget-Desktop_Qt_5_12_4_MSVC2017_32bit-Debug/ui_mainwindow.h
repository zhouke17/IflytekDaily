/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 5.12.4
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralWidget;
    QPushButton *UpDwon_pushButton;
    QPushButton *Drag_pushButton;
    QPushButton *DrapEventFilter_pushButton;
    QPushButton *OverrideTreeWidget_pushButton;
    QPushButton *Origin_pushButton;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName(QString::fromUtf8("MainWindow"));
        MainWindow->resize(400, 300);
        centralWidget = new QWidget(MainWindow);
        centralWidget->setObjectName(QString::fromUtf8("centralWidget"));
        UpDwon_pushButton = new QPushButton(centralWidget);
        UpDwon_pushButton->setObjectName(QString::fromUtf8("UpDwon_pushButton"));
        UpDwon_pushButton->setGeometry(QRect(60, 100, 75, 23));
        Drag_pushButton = new QPushButton(centralWidget);
        Drag_pushButton->setObjectName(QString::fromUtf8("Drag_pushButton"));
        Drag_pushButton->setGeometry(QRect(160, 100, 75, 23));
        DrapEventFilter_pushButton = new QPushButton(centralWidget);
        DrapEventFilter_pushButton->setObjectName(QString::fromUtf8("DrapEventFilter_pushButton"));
        DrapEventFilter_pushButton->setGeometry(QRect(270, 180, 75, 23));
        OverrideTreeWidget_pushButton = new QPushButton(centralWidget);
        OverrideTreeWidget_pushButton->setObjectName(QString::fromUtf8("OverrideTreeWidget_pushButton"));
        OverrideTreeWidget_pushButton->setGeometry(QRect(140, 180, 121, 23));
        Origin_pushButton = new QPushButton(centralWidget);
        Origin_pushButton->setObjectName(QString::fromUtf8("Origin_pushButton"));
        Origin_pushButton->setGeometry(QRect(60, 180, 75, 23));
        MainWindow->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(MainWindow);
        menuBar->setObjectName(QString::fromUtf8("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 400, 23));
        MainWindow->setMenuBar(menuBar);
        mainToolBar = new QToolBar(MainWindow);
        mainToolBar->setObjectName(QString::fromUtf8("mainToolBar"));
        MainWindow->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(MainWindow);
        statusBar->setObjectName(QString::fromUtf8("statusBar"));
        MainWindow->setStatusBar(statusBar);

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QApplication::translate("MainWindow", "MainWindow", nullptr));
        UpDwon_pushButton->setText(QApplication::translate("MainWindow", "\344\270\212\344\270\213\347\247\273\345\212\250", nullptr));
        Drag_pushButton->setText(QApplication::translate("MainWindow", "\346\213\226\345\212\250\347\247\273\345\212\250", nullptr));
        DrapEventFilter_pushButton->setText(QApplication::translate("MainWindow", "\346\213\226\346\213\275\347\233\221\346\216\247", nullptr));
        OverrideTreeWidget_pushButton->setText(QApplication::translate("MainWindow", "\351\207\215\345\206\231\346\213\226\346\213\275", nullptr));
        Origin_pushButton->setText(QApplication::translate("MainWindow", "\345\216\237\345\247\213\346\213\226\346\213\275", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
