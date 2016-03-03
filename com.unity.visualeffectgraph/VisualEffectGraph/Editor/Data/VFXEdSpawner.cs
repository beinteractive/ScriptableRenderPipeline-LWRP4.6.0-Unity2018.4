using UnityEngine;
using UnityEditor;
using System.Collections;
using Object = UnityEngine.Object;
using System;

namespace UnityEditor.Experimental
{
    internal abstract class VFXEdSpawner
    {
        public readonly Vector2 m_canvasPosition;

        public VFXEdSpawner(Vector2 position)
        {
            m_canvasPosition = position;
        }

        public abstract void Spawn();

    }


    internal class VFXEdContextNodeSpawner : VFXEdSpawner
    {
        VFXEdDataSource m_DataSource;
        VFXEdCanvas m_Canvas;
        VFXEdContext m_Context;

        public VFXEdContextNodeSpawner(VFXEdDataSource datasource, VFXEdCanvas canvas, Vector2 position, VFXEdContext context)
            : base (position)
        {
            m_DataSource = datasource;
            m_Canvas = canvas;
            m_Context = context;
        }

        public override void Spawn()
        {
            m_DataSource.AddElement(new VFXEdContextNode(m_canvasPosition, m_Context, m_DataSource));
            m_Canvas.ReloadData();
        }

    }

    internal class VFXEdOutputNodeSpawner : VFXEdSpawner
    {
        VFXEdDataSource m_DataSource;
        VFXEdCanvas m_Canvas;
        int m_Mode;
        public VFXEdOutputNodeSpawner(VFXEdDataSource datasource, VFXEdCanvas canvas, Vector2 position, int mode)
            : base (position)
        {
            m_DataSource = datasource;
            m_Canvas = canvas;
            m_Mode = mode;
        }

        public override void Spawn()
        {
            m_DataSource.AddElement(new VFXEdOutputNode(m_canvasPosition, m_DataSource,m_Mode));
            m_Canvas.ReloadData();
        }

    }

    internal class VFXEdTriggerNodeSpawner : VFXEdSpawner
    {
        VFXEdDataSource m_DataSource;
        VFXEdCanvas m_Canvas;

        public VFXEdTriggerNodeSpawner(VFXEdDataSource datasource, VFXEdCanvas canvas, Vector2 position)
            : base (position)
        {
            m_DataSource = datasource;
            m_Canvas = canvas;
        }

        public override void Spawn()
        {
            m_DataSource.AddElement(new VFXEdTriggerNode(m_canvasPosition, m_DataSource));
            m_Canvas.ReloadData();
        }

    }

    internal class VFXEdEventNodeSpawner : VFXEdSpawner
    {
        VFXEdDataSource m_DataSource;
        VFXEdCanvas m_Canvas;
        string m_EventName;

        public VFXEdEventNodeSpawner(VFXEdDataSource datasource, VFXEdCanvas canvas, Vector2 position, string eventname)
            : base (position)
        {
            m_DataSource = datasource;
            m_Canvas = canvas;
            m_EventName = eventname;
        }

        public override void Spawn()
        {
            m_DataSource.AddElement(new VFXEdEventNode(m_canvasPosition, m_DataSource,m_EventName));
            m_Canvas.ReloadData();
        }

    }

    internal class VFXEdDataNodeSpawner : VFXEdSpawner
    {
        VFXEdDataSource m_DataSource;
        VFXEdCanvas m_Canvas;

        public VFXEdDataNodeSpawner(VFXEdDataSource datasource, VFXEdCanvas canvas, Vector2 position)
            : base (position)
        {
            m_DataSource = datasource;
            m_Canvas = canvas;
        }

        public override void Spawn()
        {
            m_DataSource.AddElement(new VFXEdDataNode(m_canvasPosition, m_DataSource));
            m_Canvas.ReloadData();
        }

    }


    internal class VFXEdProcessingNodeBlockSpawner : VFXEdSpawner
    {
        VFXEdContextNode m_Node;
        VFXBlock m_Block;
        VFXEdDataSource m_DataSource;

        public VFXEdProcessingNodeBlockSpawner(Vector2 position, VFXBlock block, VFXEdContextNode node, VFXEdDataSource datasource)
            : base (position)
        {
            m_Block = block;
            m_Node = node;
            m_DataSource = datasource;
        }

        public override void Spawn()
        {
            m_Node.NodeBlockContainer.CaptureDrop = true;
            m_Node.NodeBlockContainer.UpdateCaptureDrop(m_canvasPosition);
            m_Node.NodeBlockContainer.AcceptDrop(new VFXEdProcessingNodeBlock(m_Block, m_DataSource));
        }

    }

    internal class VFXEdDataNodeBlockSpawner : VFXEdSpawner
    {
        VFXEdDataNode m_Node;
        VFXDataBlock m_DataBlock;
        VFXEdDataSource m_DataSource;

        public VFXEdDataNodeBlockSpawner(Vector2 position, VFXDataBlock datablock, VFXEdDataNode node, VFXEdDataSource datasource)
            : base (position)
        {
            m_DataBlock = datablock;
            m_Node = node;
            m_DataSource = datasource;
        }

        public override void Spawn()
        {
            m_Node.NodeBlockContainer.AddNodeBlock(new VFXEdDataNodeBlock(m_DataBlock,m_DataSource));
           
        }

    }

}
