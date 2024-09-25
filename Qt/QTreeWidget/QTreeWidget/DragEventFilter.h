#ifndef DRAGEVENTFILTER_H
#define DRAGEVENTFILTER_H

#include <QApplication>
#include <QTreeWidget>
#include <QTreeWidgetItem>
#include <QDrag>
#include <QMimeData>
#include <QMouseEvent>
#include <QDropEvent>
#include <QDebug>
#include <QPropertyAnimation>
#include <QRect>
#include <QPainter>
#include <QPen>
#include "customdragindicator.h"

class DragEventFilter: public QObject {
    Q_OBJECT
public:
    DragEventFilter(QTreeWidget *treeWidget, QObject *parent = nullptr,CustomDragIndicator *indicator = nullptr)
        : QObject(parent), treeWidget(treeWidget), dragItem(nullptr),dragIndicator(indicator){}

protected:
    bool eventFilter(QObject *obj, QEvent *event) override {
        if (event->type() == QEvent::MouseButtonPress) {
            QMouseEvent *mouseEvent = static_cast<QMouseEvent*>(event);
            if (mouseEvent->button() == Qt::LeftButton) {
                dragItem = treeWidget->itemAt(mouseEvent->pos());
//                dragItemIndex = dragItem->parent()->indexOfChild(dragItem);
//                qDebug() << "源索引:" + QString::number(dragItemIndex);
                if(dragItem->childCount() == 0)
                {
                    if (dragItem) {
                        QDrag *drag = new QDrag(treeWidget);
                        QMimeData *mimeData = new QMimeData;
                        mimeData->setText(dragItem->text(0)); // 设置拖拽数据为项的文本
                        qDebug() << "item name" << dragItem->text(0);
                        drag->setMimeData(mimeData);

                        //设置拖拽图标
//                        QPixmap dragPixmap = QPixmap(":/image/1.jpg");
//                        drag->setPixmap(dragPixmap);

//                        // Create custom drag icon
//                        QPixmap pixmap(50, 2);  // Define size for the pixmap
//                        pixmap.fill(Qt::transparent);  // Start with a transparent pixmap
//                        QPainter painter(&pixmap);
//                        painter.setPen(QPen(Qt::white, 20, Qt::SolidLine));
//                        painter.drawLine(mouseEvent->pos().x(), mouseEvent->pos().y(), 100, 2);

//                        // Set the pixmap as the drag icon and adjust the hotspot
//                        drag->setPixmap(pixmap);
//                        drag->setHotSpot(QPoint(25, 25));  // Adjust hotspot to center of the pixmap

                        QPixmap pixmap(1, 1);
                        pixmap.fill(Qt::darkYellow);
                        drag->setPixmap(pixmap);


                        drag->exec(Qt::MoveAction);
                    }
                }
                else {
                    dragItem->setExpanded(dragItem->isExpanded() ? false : true);
                }
                return true;
            }
        }
        else if (event->type() == QEvent::DragMove) {
            auto *dragMoveEvent = static_cast<QDragMoveEvent *>(event);
            QTreeWidgetItem *targetItem = treeWidget->itemAt(dragMoveEvent->pos());
            if (targetItem && targetItem->parent() == dragItem->parent()) {
                dragMoveEvent->accept();

                // Show custom drag indicator
                dragIndicator->setGeometry(QRect(0, dragMoveEvent->pos().y() + 20, 300, 1));
                dragIndicator->show();
            } else {
                dragMoveEvent->ignore();
                dragIndicator->hide();
            }
            return true;
        }
        else if (event->type() == QEvent::DragEnter) {
            QDragEnterEvent *dragEnterEvent = static_cast<QDragEnterEvent*>(event);
            if (dragEnterEvent->mimeData()->hasText())
            {
                qDebug() << "can drap";
                dragEnterEvent->acceptProposedAction();
                return true;
            }
        } else if (event->type() == QEvent::Drop) {
            QDropEvent *dropEvent = static_cast<QDropEvent*>(event);
            QString text = dropEvent->mimeData()->text();
            QTreeWidgetItem *targetItem = treeWidget->itemAt(dropEvent->pos());

            if (targetItem) {
                QTreeWidgetItem *draggedItem = dragItem;
                if (draggedItem)
                {
                    qDebug() << "draggedItem:" << draggedItem->text(0);
                    if (targetItem->parent() == draggedItem->parent() && draggedItem != targetItem)
                    {
                        QTreeWidgetItem *parent = targetItem->parent();
                        if (parent) {
                            int index = parent->indexOfChild(targetItem);
                            parent->removeChild(draggedItem);
                            qDebug() << "dragItem index:" << parent->indexOfChild(targetItem);
                            parent->insertChild(index, draggedItem);
                        }
                        else {
                            treeWidget->takeTopLevelItem(treeWidget->indexOfTopLevelItem(draggedItem));
                            treeWidget->addTopLevelItem(draggedItem);
                        }
                        treeWidget->setCurrentItem(draggedItem);
                    }
                }
                dropEvent->acceptProposedAction();
                dragIndicator->hide();
                return true;
            }
        }
        return QObject::eventFilter(obj, event);
    }

private:
    QTreeWidget *treeWidget;
    QTreeWidgetItem *dragItem;
    int dragItemIndex;

    QTreeWidgetItem* findItemByText(const QString &text) {
        QList<QTreeWidgetItem*> items = treeWidget->findItems(text, Qt::MatchExactly);
        return items.isEmpty() ? nullptr : items.first();
    }

    QRect m_rect;

     CustomDragIndicator *dragIndicator;
};

#endif // DRAGEVENTFILTER_H
