#ifndef DRAGTREEITEMWIDGET_H
#define DRAGTREEITEMWIDGET_H

#include <QWidget>
#include <QTreeWidget>
#include <QTreeWidgetItem>
#include <QDrag>
#include <QMimeData>
#include <QDropEvent>
#include <QMimeData>
#include <QDragEnterEvent>
#include <QDragMoveEvent>
#include <QDropEvent>
#include "customdragindicator.h"


class DragTreeItemWidget : public QTreeWidget
{
    Q_OBJECT
public:
    explicit DragTreeItemWidget(QWidget *parent = nullptr){
           CustomDragIndicator *indicator = new CustomDragIndicator(this);
           dragIndicator = indicator;}

private:
    CustomDragIndicator *dragIndicator;
signals:

public slots:

protected:
    void startDrag(Qt::DropActions supportedActions)  override{
        item = currentItem();
        if (!item) return;

        QDrag *drag = new QDrag(this);
        QMimeData *mimeData = new QMimeData;

        // 将拖拽的项目索引信息存储在 MIME 数据中
        mimeData->setText(item->text(0));
        drag->setMimeData(mimeData);

        drag->exec(Qt::MoveAction);
    }

    void dragEnterEvent(QDragEnterEvent *event) override {
        if (event->mimeData()->hasText()) {
            event->acceptProposedAction();
        } else {
            event->ignore();
        }
    }

    void dragMoveEvent(QDragMoveEvent *event) override {
        if (event->mimeData()->hasText()) {
            event->accept();
            auto *dragMoveEvent = static_cast<QDragMoveEvent *>(event);
            // Show custom drag indicator
            dragIndicator->setGeometry(QRect(0, dragMoveEvent->pos().y() + 20, 300, 1));
            dragIndicator->show();
        } else {
            event->ignore();
        }
    }

    void dropEvent(QDropEvent *event) override {
        QString text = event->mimeData()->text();
        QTreeWidgetItem *targetItem = itemAt(event->pos());

        if (targetItem) {
            QTreeWidgetItem *draggedItem = item;

            if (draggedItem) {
                // 移动项目到目标项下
                QTreeWidgetItem *parent = targetItem->parent();
                if (parent) {
                     int index = parent->indexOfChild(targetItem);
                    parent->removeChild(draggedItem);
                    parent->insertChild(index, draggedItem);
                } else {
                    // 项目为顶级
                    takeTopLevelItem(indexOfTopLevelItem(draggedItem));
                    addTopLevelItem(draggedItem);
                }
                setCurrentItem(draggedItem);
                dragIndicator->hide();
            }
        }

        event->acceptProposedAction();
    }

private:
    QTreeWidgetItem* findItemByText(const QString &text) {
        QList<QTreeWidgetItem*> items = findItems(text, Qt::MatchExactly);
        return items.isEmpty() ? nullptr : items.first();
    }

private:
    QTreeWidgetItem *item;
};

#endif // DRAGTREEITEMWIDGET_H
