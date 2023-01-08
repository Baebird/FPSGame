using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public List<InputPackage> OnLeftClickDownPackages = new List<InputPackage>();
    public List<InputPackage> OnLeftClickHoldPackages = new List<InputPackage>();
    public List<InputPackage> OnLeftClickUpPackages = new List<InputPackage>();

    public List<InputPackage> OnRightClickDownPackages = new List<InputPackage>();

    public List<InputPackage> OnSpaceDownPackages = new List<InputPackage>();

    public List<InputPackage> OnShiftDownPackages = new List<InputPackage>();
    public List<InputPackage> OnShiftHoldPackages = new List<InputPackage>();
    public List<InputPackage> OnShiftUpPackages = new List<InputPackage>();

    public List<InputPackage> OnLeftCtrlDownPackages = new List<InputPackage>();
    public List<InputPackage> OnLeftCtrlUpPackages = new List<InputPackage>();

    public List<InputPackage> OnRDownPackages = new List<InputPackage>();

    public List<InputPackage> OnEDownPackages = new List<InputPackage>();
    public List<InputPackage> OnEHoldPackages = new List<InputPackage>();
    public List<InputPackage> OnEUpPackages = new List<InputPackage>();

    public List<InputPackage> OnQDownPackages = new List<InputPackage>();

    public List<InputPackage> OnWDownPackages = new List<InputPackage>();
    public List<InputPackage> OnWHoldPackages = new List<InputPackage>();
    public List<InputPackage> OnWUpPackages = new List<InputPackage>();

    public List<InputPackage> OnADownPackages = new List<InputPackage>();
    public List<InputPackage> OnAHoldPackages = new List<InputPackage>();
    public List<InputPackage> OnAUpPackages = new List<InputPackage>();

    public List<InputPackage> OnSDownPackages = new List<InputPackage>();
    public List<InputPackage> OnSHoldPackages = new List<InputPackage>();
    public List<InputPackage> OnSUpPackages = new List<InputPackage>();

    public List<InputPackage> OnDDownPackages = new List<InputPackage>();
    public List<InputPackage> OnDHoldPackages = new List<InputPackage>();
    public List<InputPackage> OnDUpPackages = new List<InputPackage>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (OnLeftClickDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnLeftClickDownPackages);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (OnLeftClickHoldPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnLeftClickHoldPackages);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (OnLeftClickUpPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnLeftClickUpPackages);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (OnRightClickDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnRightClickDownPackages);
            }
        }
        if (Input.GetKey("left shift"))
        {
            if (OnShiftHoldPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnShiftHoldPackages);
            }
        }
        if (Input.GetKeyDown("left shift"))
        {
            if (OnShiftDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnShiftDownPackages);
            }
        }
        if (Input.GetKeyUp("left shift"))
        {
            if (OnShiftUpPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnShiftUpPackages);
            }
        }
        if (Input.GetKeyDown("left ctrl"))
        {
            if (OnLeftCtrlDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnLeftCtrlDownPackages);
            }
        }
        if (Input.GetKeyUp("left ctrl"))
        {
            if (OnLeftCtrlUpPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnLeftCtrlUpPackages);
            }
        }
        if (Input.GetKeyDown("space"))
        {
            if (OnSpaceDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnSpaceDownPackages);
            }
        }
        if (Input.GetKeyDown("w"))
        {
            if (OnWDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnWDownPackages);
            }
        }
        if (Input.GetKey("w"))
        {
            if (OnWHoldPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnWHoldPackages);
            }
        }
        if (Input.GetKeyUp("w"))
        {
            if (OnWUpPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnWUpPackages);
            }
        }
        if (Input.GetKeyDown("a"))
        {
            if (OnADownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnADownPackages);
            }
        }
        if (Input.GetKey("a"))
        {
            if (OnAHoldPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnAHoldPackages);
            }
        }
        if (Input.GetKeyUp("a"))
        {
            if (OnAUpPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnAUpPackages);
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (OnSDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnSDownPackages);
            }
        }
        if (Input.GetKey("s"))
        {
            if (OnSHoldPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnSHoldPackages);
            }
        }
        if (Input.GetKeyUp("s"))
        {
            if (OnSUpPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnSUpPackages);
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (OnDDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnDDownPackages);
            }
        }
        if (Input.GetKey("d"))
        {
            if (OnDHoldPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnDHoldPackages);
            }
        }
        if (Input.GetKeyUp("d"))
        {
            if (OnDUpPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnDUpPackages);
            }
        }
        if (Input.GetKeyDown("r"))
        {
            if (OnRDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnRDownPackages);
            }
        }
        if (Input.GetKeyDown("e"))
        {
            if (OnEDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnEDownPackages);
            }
        }
        if (Input.GetKey("e"))
        {
            if (OnEHoldPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnEHoldPackages);
            }
        }
        if (Input.GetKey("e"))
        {
            if (OnEUpPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnEUpPackages);
            }
        }
        if (Input.GetKeyDown("q"))
        {
            if (OnQDownPackages.Count > 0)
            {
                RunPackagesHighestPriority(OnQDownPackages);
            }
        }
    }
    void RunPackagesHighestPriority(List<InputPackage> packages)
    {
        List<int> priorities = new List<int>();
        List<InputPackage> packageListCopy = new List<InputPackage>(packages);
        foreach (InputPackage package in packageListCopy)
        {
            priorities.Add(package.priority);
        }
        priorities.Sort();

        int highestPriorityInPackageList = priorities[priorities.Count - 1];
        foreach (InputPackage package in packageListCopy)
        {
            if (package.priority == highestPriorityInPackageList)
            {
                package.pointer();
            }
        }
    }
}
