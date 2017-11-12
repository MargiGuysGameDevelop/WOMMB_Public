using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface FigureActionListener
{
    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    void Movement(float _x, float _y);
    /// <summary>
    /// 停止移動
    /// </summary>
    /// <param name="_x"></param>
    /// <param name="_y"></param>
    void StopMovement(float _x, float _y);
}