﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// This class enables Drag and Drop Behaviour for the game object it is attached to. 
/// It uses other script - DraggingActions to determine whether we can drag this game object now or not and 
/// whether the drop was successful or not.
/// </summary>

public class Draggable : MonoBehaviour {


//DS added these from Menu course
   public enum StartDragBehavior
    {
        OnMouseDown, InAwake
    }

    public enum EndDragBehavior
    {
        OnMouseUp, OnMouseDown
    }

    public StartDragBehavior HowToStart = StartDragBehavior.OnMouseDown;
    public EndDragBehavior HowToEnd = EndDragBehavior.OnMouseUp;

//DS

    // PRIVATE FIELDS

    // a flag to know if we are currently dragging this GameObject
    private bool dragging = false;

    // distance from the center of this Game Object to the point where we clicked to start dragging 
    private Vector3 pointerDisplacement;

    // distance from camera to mouse on Z axis 
    private float zDisplacement;

    // reference to DraggingActions script. Dragging Actions should be attached to the same GameObject.
    private DraggingActions da;

    // STATIC property that returns the instance of Draggable that is currently being dragged
    private static Draggable _draggingThis;
    public static Draggable DraggingThis
    {
        get{ return _draggingThis;}
    }

    // MONOBEHAVIOUR METHODS
    void Awake()
    {
        da = GetComponent<DraggingActions>();
    }

    void OnMouseDown()
    {
        if (da!=null && da.CanDrag)
        {
   
            dragging = true;
            // when we are dragging something, all previews should be off
            //DS HoverPreview.PreviewsAllowed = false;
            _draggingThis = this;
            da.OnStartDrag();
            zDisplacement = -Camera.main.transform.position.z + transform.position.z;
            pointerDisplacement = -transform.position + MouseInWorldCoords();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (dragging)
        { 

            Vector3 mousePos = MouseInWorldCoords();
            //Debug.Log(mousePos);
            transform.position = new Vector3(mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);   
            
            da.OnDraggingInUpdate();
        }
    }
	
    void OnMouseUp()
    {
        if (dragging)
        {
            dragging = false;
            // turn all previews back on
            //DS HoverPreview.PreviewsAllowed = true;
            _draggingThis = null;
            da.OnEndDrag();
        }
    }   

    // returns mouse position in World coordinates for our GameObject to follow. 
    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        //Debug.Log(screenMousePos);
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }

//DS Added these from menu course
    public void StartDragging()
    {
        dragging = true;
        // when we are dragging something, all previews should be off
        HoverPreview.PreviewsAllowed = false;
        _draggingThis = this;
        da.OnStartDrag();
        zDisplacement = -Camera.main.transform.position.z + transform.position.z;
        pointerDisplacement = -transform.position + MouseInWorldCoords();
    }

    public void CancelDrag()
    {
        if (dragging)
        {
            dragging = false;
            // turn all previews back on
            HoverPreview.PreviewsAllowed = true;
            _draggingThis = null;
            da.OnCancelDrag();
        }
    }

//DS
        
}
