#include "TreeItemMimeData.h"

void TreeItemMimeData::SetDragData(QString mimeType, QTreeWidgetItem *pItem)
{
    m_format << mimeType;
    m_pDragItem = pItem;
}

QVariant TreeItemMimeData::retrieveData(const QString &mimetype, QVariant::Type preferredType) const
{
    if( mimetype == "ItemMimeData")
    {
        return m_pDragItem;
    }
    else
        return QMimeData::retrieveData(mimetype,preferredType);
}
