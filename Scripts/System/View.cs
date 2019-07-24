using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//it may be not the best architecture
//but who care it


public class View : MonoBehaviour
{
    Vector3 center = new Vector3(0, 0, 0);

    bool on_right_down = false;
    Vector3 mouse_position;

    // Start is called before the first frame update
    void Start()
    {
        //print("wtf");
        center = new Vector3(0, 0, 0);
        this.transform.LookAt(center);
    }

    // Update is called once per frame
    void Update()
    {

        //this.transform.Translate(Vector3.up * 0.1f, Space.World);

        if (Input.GetMouseButtonDown(1))
        {
            on_right_down = true;
            mouse_position = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1)) on_right_down = false;

        if (on_right_down)
        {
            //Debug.Log(Input.mousePosition);
            Vector3 now_position = this.transform.position - center;
            //float length = Vector3.Distance(now_position, Vector3.zero);
            //Vector3 forward = Vector3.Normalize(now_position);
            //Vector3 up = new Vector3(-1 * forward.y, forward.x, forward.z);
            //Vector3 right = new Vector3(-1 * forward.z, forward.y, forward.x);
            //Vector3 mouse_move = (Input.mousePosition - mouse_position) * Time.deltaTime * 0.3f;

            //forward = forward + mouse_move.x * right + mouse_move.y * up;
            //forward = rotate_vector3(forward, (Input.mousePosition - mouse_position) * Time.deltaTime * 0.3f);
            //forward = Vector3.Normalize(forward) * length;
            //this.transform.position = center + forward;
            //Debug.Log(now_position);
            now_position = rotate_vector3(now_position, (Input.mousePosition - mouse_position) * Time.deltaTime * 0.3f);
            this.transform.position = center + now_position;
            this.transform.LookAt(center);

            mouse_position = Input.mousePosition;

           

        }

        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if(wheel != 0)
        {
            Vector3 now_position = this.transform.position - center;
            float length = Vector3.Distance(now_position, Vector3.zero);
            now_position = Vector3.Normalize(now_position) * (length - wheel * 100);
            this.transform.position = center + now_position;
        }

    }

    Vector3 rotate_vector3(Vector3 source, Vector3 rot)
    {
        source = new Vector3(source.x * Mathf.Cos(rot.x) + source.z * Mathf.Sin(rot.x),
            source.y, source.z * Mathf.Cos(rot.x) - source.x * Mathf.Sin(rot.x));
        float v = Mathf.Sqrt(source.x * source.x + source.z * source.z);
        float sin = source.x / v;
        float cos = source.z / v;
        float h = source.y * Mathf.Cos(rot.y) + v * Mathf.Sin(rot.y);
        v = v * Mathf.Cos(rot.y) - source.y * Mathf.Sin(rot.y);
        source = new Vector3(v * sin, h, v * cos);
        
        return source;
    }
}
