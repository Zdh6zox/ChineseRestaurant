using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//目前对与桌子类的想法：
//1.一张桌子能够容纳的椅子空间是有上限的，而一张椅子能够容纳的人是固定的。
//2.一开始一套桌椅会有默认配置。
//3.如果一组客人想要坐这张桌子但是目前椅子能够容纳的不足，可以搬来空闲的椅子来就坐，或者让玩家加备用椅子。但是这样会影响其他桌子的容纳人数
public class Table : MonoBehaviour
{
    [System.Serializable]
    public enum TableType
    {
        TableType_Square,
        TableType_Round
    }

    public TableType _TableType;

    public bool _IsTaken = false;

    public float _LongEdgeLength; //如果是圆桌则以长边当作半径

    public float _ShortEdgeLength;

    public float _MaxSeatSpaceLength = 8.0f; //用椅子的空间长度来确定最多能够容纳的椅子数量

    private float _currentUsedSeatSpaceLength = 0.0f;

    public List<Seat> _CurrentSeats = new List<Seat>();
    // Start is called before the first frame update
    void Start()
    {
        Utils.GetGameManager().GetInnManager().RegisterTable(this);

        _currentUsedSeatSpaceLength = 0.0f;
        //initial seats
        foreach (Transform child in transform)
        {
            Seat _seat = child.gameObject.GetComponent<Seat>();
            if (_seat != null)
                _CurrentSeats.Add(_seat);
        }

        //ungroup all seats
        foreach (Seat child in _CurrentSeats)
        {
            child.gameObject.transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool AssignCustomer()
    {
        return false;
    }

    public Vector3 PopOutSeats(Customer requestingCustomer)
    {
        foreach(Seat seat in _CurrentSeats)
        {
            if (seat._IsTaken == true)
                continue;

            seat.AssignCustomer(requestingCustomer);
            return seat.transform.position;
        }

        return Vector3.positiveInfinity;
    }

    public int GetAvaliableSeatsCount()
    {
        int count = 0;
        foreach (Seat seat in _CurrentSeats)
        {
            if (seat._IsTaken == true)
                continue;

            count++;
        }

        return count;
    }

    public bool AddNewSeat(Seat newSeat)
    {
        if (_MaxSeatSpaceLength <= _currentUsedSeatSpaceLength + newSeat._SeatSpace)
        {
            Debug.Log(string.Format("table {0} has no room for seat {1}", this.name, newSeat.name));
            return false;
        }

        _CurrentSeats.Add(newSeat);

        ArrangeSeatsLocation();
        return true;
    }

    private void ArrangeSeatsLocation()
    {
        //Because we allow to add seats on the fly,
        //so we must dynamically arrange seats' location based on current seats

        switch (_TableType)
        {
            case TableType.TableType_Square:
                ArrangeSeatForSquareTable();
                break;
            case TableType.TableType_Round:
                ArrangeSeatForRoundTable();
                break;
        }
    }

    private void ArrangeSeatForSquareTable()
    {
        //first if seat number is odd
        bool isOddNum = _CurrentSeats.Count % 2 == 0 ? false : true;

        if(isOddNum)
        {
            foreach (Seat seat in _CurrentSeats)
            {

            }
        }
        else
        {

        }
    }

    private void ArrangeSeatForRoundTable()
    {
        foreach (Seat seat in _CurrentSeats)
        {

        }
    }

    public void OnCustomerLeave()
    {
        foreach (Seat seat in _CurrentSeats)
        {
            seat.OnCustomerLeave();
        }
    }
}
