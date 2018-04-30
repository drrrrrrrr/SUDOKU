﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ngSignalRSudoku.Models;

namespace ngSignalRSudoku
{
    [HubName("sudokuHub")]
    public class SudokuHub : Hub
    {
        //public static List<Group> _Gropus = new List<Group>();

        public SudokuHub()
        {
            if (!db.Gropus.Any(x => x.Name == "Global"))
                db.Gropus.Add(new Group("Global", "System"));
        }

        public void CreateGroup(string name, string userName)
        {
            if (!db.Gropus.Any(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
                db.Gropus.Add(new Group(name, userName));

            Groups.Add(Context.ConnectionId, name);

            Clients.All.groupCreated(db.Gropus);
        }
        public void GetGroups()
        {
            Clients.All.getGroups(db.Gropus);
        }

        public void GetSudoku(string groupName)
        {
            var group = db.Gropus.FirstOrDefault(x => x.Name.Equals(groupName, StringComparison.CurrentCultureIgnoreCase));

            //Add the user to this group while getting Sudoku.//
            Groups.Add(Context.ConnectionId, groupName);

            Clients.Caller.getSudoku(db.GetSudoku(groupName));

          
        }
        
        public void UpdateCell(string groupName, GridCell cell)
        {
            db.UpdateSudoku(groupName, cell);
            var sudoku = db.GetSudoku(groupName);  // updated sudoku.//
            var updatedCell = sudoku.Grid[cell.RowIndex].Cells[cell.ColIndex];
           
            if(Check(groupName,cell))
            {
                updatedCell.Freezed = true;
                Clients.Group(groupName).cellUpdate(updatedCell, sudoku);
            }
            
            // Trigger the update to all users of that Group.//
           
        }
        public bool Check(string groupName, GridCell cel)
        {
            Sudoku sudoku = db.GetSolvedSudoku(groupName);
            if (sudoku.Grid[cel.RowIndex].Cells[cel.ColIndex].Data == cel.Data)
            {
                return true;
            }
            return false;
        }
    }
}