Public Class Form1
    'input
    Private burstTimeArray As New ArrayList()
    Private arrivalTimeArray As New ArrayList()
    Private processIdArray As New ArrayList()
    Dim buffer1 As New ArrayList

    Private startTimeArray As New ArrayList()
    Private stopTimeArray As New ArrayList()
    Private complitionTime As New ArrayList()
    Private turnAroundTime As New ArrayList()
    Private waitingTime As New ArrayList()
    Private responseTime As New ArrayList()

    Private prioritiesArray As New ArrayList()
    Private startTime_stopArray As New ArrayList()
    Private processesArray As New ArrayList()
    Private clearedIndexes As New ArrayList
    Private waitingQueue As New ArrayList
    Private readyQueueArray_SJN As New ArrayList
    Private burstTimeBufferArray_SJN As New ArrayList
    Private quantumTime As Integer


    Private source As New ArrayList()
    Private source1 As New ArrayList()


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        ComboBox2.Enabled = False

        source.Add("- ---------- Select type -----------")
        source.Add("Non Preemptive")
        source.Add("Preemptive")
        source.Add("Both")

        source1.Add("- ---------- Select type -----------")
        source1.Add("Non Preemptive")
        source1.Add("Preemptive")

        MenuStrip1.Visible = False


    End Sub


    Private Function getPriorities() As Boolean
        Dim Buffer As Integer
        Dim stat As Boolean = True
        For Index As Integer = 0 To processIdArray.Count - 1
            If Integer.TryParse(InputBox("ProcessId=" + processIdArray.Item(Index).ToString + "  Burst=" + burstTimeArray.Item(Index).ToString + "  ArrivalTime=" + arrivalTimeArray.Item(Index).ToString + "  Priority="), Buffer) Then
                prioritiesArray.Add(Buffer.ToString)
            Else

                Dim iMessage As DialogResult = MessageBox.Show("Enter the priority for " + processIdArray.Item(Index).ToString + " again Or press No to select a different scheduling algorythm", "DataGridView System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If iMessage = DialogResult.Yes Then
                    Index = Index - 1
                Else
                    prioritiesArray.Clear()

                    Index = processIdArray.Count - 1
                    stat = False
                End If
            End If
        Next


        Return stat
    End Function






    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                About.Text = " Select a sampling method to use. "
                GroupBox2.Visible = False
                TextBox4.Width = 10
                TextBox4.Visible = False
                Button1.Location = New Point(345, 169)

                ComboBox2.Enabled = False
                ComboBox2.SelectedIndex = 0
            Case 1
                About.Text = " In this nonpreemptive scheduling algorithm that handles jobs according to their arrival time. It's a 
very simple algorithm that implement FIFO queues. Optimal for most batch systems, but unacceptable for interactive systems because
interactive users expect quick response times. As a new job enters the system it'a PCB is linked to the end of the ready queue. 
It is typically FCFS system there are no wait queues ( each job is run to completion )."

                DataGridView2.Rows.Clear()

                If prioritiesArray.Count > 0 Then
                    Dim iMessage As DialogResult = MessageBox.Show("Process priorities will be discarded in this scheduling algorythm, click Yes to proceed or No to return to Priority Scheduling", "DataGridView System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If iMessage = DialogResult.Yes Then
                        prioritiesArray.Clear()


                        ComboBox2.Items.Clear()
                        ComboBox2.Items.AddRange(source.ToArray)
                        GroupBox2.Visible = True
                        Panel2.Visible = False
                        ComboBox2.SelectedIndex = 3
                        ComboBox2.Enabled = False

                        If DataGridView1.Columns.Count > 3 Then
                            DataGridView1.Columns.RemoveAt(3)
                            DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                        End If


                        TextBox4.Width = 10
                        TextBox4.Visible = False
                        Button1.Location = New Point(345, 169)
                    Else
                        ComboBox1.SelectedIndex = 3
                    End If

                Else

                    ComboBox2.Items.Clear()
                    ComboBox2.Items.AddRange(source.ToArray)
                    GroupBox2.Visible = True
                    Panel2.Visible = False
                    ComboBox2.SelectedIndex = 3
                    ComboBox2.Enabled = False

                    If DataGridView1.Columns.Count > 3 Then
                        DataGridView1.Columns.RemoveAt(3)
                        DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                    End If


                    TextBox4.Width = 10
                    TextBox4.Visible = False
                    Button1.Location = New Point(345, 169)
                End If

                TextBox1.Select()

            Case 2
                About.Text = " Shortest Job First (SJF) is an algorithm in which the process having the smallest execution time is 
chosen for the next execution. This scheduling method can be preemptive or non-preemptive. It significantly reduces the average waiting 
time for other processes awaiting execution. The full form of SJF is Shortest Job First. "

                DataGridView2.Rows.Clear()

                If prioritiesArray.Count > 0 Then
                    Dim iMessage As DialogResult = MessageBox.Show("Process priorities will be discarded in this scheduling algorythm, click Yes to proceed or No to return to Priority Scheduling", "DataGridView System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If iMessage = DialogResult.Yes Then
                        prioritiesArray.Clear()
                        ComboBox2.Items.Clear()
                        ComboBox2.Items.AddRange(source1.ToArray)
                        GroupBox2.Visible = True
                        Panel2.Visible = False
                        ComboBox2.SelectedIndex = 1
                        ComboBox2.Enabled = True

                        If DataGridView1.Columns.Count > 3 Then
                            DataGridView1.Columns.RemoveAt(3)
                            DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                        End If

                        TextBox4.Width = 10
                        TextBox4.Visible = False
                        Button1.Location = New Point(345, 169)
                        TextBox1.Select()
                    Else
                        ComboBox1.SelectedIndex = 3
                    End If
                Else

                    ComboBox2.Items.Clear()
                    ComboBox2.Items.AddRange(source1.ToArray)
                    GroupBox2.Visible = True
                    Panel2.Visible = False
                    ComboBox2.SelectedIndex = 1
                    ComboBox2.Enabled = True

                    If DataGridView1.Columns.Count > 3 Then
                        DataGridView1.Columns.RemoveAt(3)
                        DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                    End If

                    TextBox4.Width = 10
                    TextBox4.Visible = False
                    Button1.Location = New Point(345, 169)
                    TextBox1.Select()


                End If

            Case 3
                About.Text = "  Priority Scheduling is a method of scheduling processes that is based on priority. In this algorithm, 
the scheduler selects the tasks to work as per the priority.

The processes with higher priority should be carried out first, whereas jobs with equal priorities are carried out on a round-robin or 
FCFS basis. Priority depends upon memory requirements, time requirements, etc.

This algorythm uses FCFS in such cases. Also, the lesser the number, the higher the priorirt."

                DataGridView2.Rows.Clear()

                If arrivalTimeArray.Count > 0 Or burstTimeArray.Count > 0 Or processesArray.Count > 0 Then

                    If prioritiesArray.Count > 0 Then

                        refreshData()
                        ComboBox2.Items.Clear()
                        ComboBox2.Items.AddRange(source1.ToArray)
                        GroupBox2.Visible = True
                        Panel2.Visible = False
                        ComboBox2.SelectedIndex = 1
                        ComboBox2.Enabled = True

                        TextBox4.Width = 105
                        TextBox4.Visible = True

                        Button1.Location = New Point(455, 169)
                        DataGridView1.Columns.Item(2).HeaderText = "A T"

                        If DataGridView1.Columns.Count < 4 Then
                            DataGridView1.Columns.Add("priority", "Priority")
                        End If


                        DataGridView1.Columns.Item(2).ToolTipText = "Arrival Time"
                    Else
                        Dim iMessage As DialogResult = MessageBox.Show("Confirm if you want to insert priorities for the processes without priorities Or click No to clear all", "DataGridView System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        If iMessage = DialogResult.No Then
                            arrivalTimeArray.Clear()
                            burstTimeArray.Clear()
                            processIdArray.Clear()

                            refreshData()
                            ComboBox2.Items.Clear()
                            ComboBox2.Items.AddRange(source1.ToArray)
                            GroupBox2.Visible = True
                            Panel2.Visible = False
                            ComboBox2.SelectedIndex = 1
                            ComboBox2.Enabled = True


                            TextBox4.Width = 105
                            TextBox4.Visible = True

                            Button1.Location = New Point(455, 169)
                            If DataGridView1.Columns.Count < 4 Then
                                DataGridView1.Columns.Add("priority", "Priority")
                            End If

                            DataGridView1.Columns.Item(2).HeaderText = "A T"
                            DataGridView1.Columns.Item(2).ToolTipText = "Arrival Time"

                        Else
                            If getPriorities() Then
                                ComboBox2.Items.Clear()
                                ComboBox2.Items.AddRange(source1.ToArray)

                                GroupBox2.Visible = True
                                ComboBox2.SelectedIndex = 1
                                ComboBox2.Enabled = True
                                Panel2.Visible = False

                                TextBox4.Width = 105
                                TextBox4.Visible = True

                                Button1.Location = New Point(455, 169)
                                If DataGridView1.Columns.Count < 4 Then
                                    DataGridView1.Columns.Add("priority", "Priority")
                                End If

                                DataGridView1.Columns.Item(2).HeaderText = "A T"
                                DataGridView1.Columns.Item(2).ToolTipText = "Arrival Time"
                                refreshData()
                            Else
                                ComboBox1.SelectedIndex = 1
                            End If

                        End If
                    End If



                Else
                    ComboBox2.Items.Clear()
                    ComboBox2.Items.AddRange(source1.ToArray)

                    GroupBox2.Visible = True
                    ComboBox2.SelectedIndex = 1
                    ComboBox2.Enabled = True
                    Panel2.Visible = False

                    TextBox4.Width = 105
                    TextBox4.Visible = True

                    Button1.Location = New Point(455, 169)
                    If DataGridView1.Columns.Count < 4 Then
                        DataGridView1.Columns.Add("priority", "Priority")
                    End If

                    DataGridView1.Columns.Item(2).HeaderText = "A T"
                    DataGridView1.Columns.Item(2).ToolTipText = "Arrival Time"
                End If
                TextBox1.Select()


            Case 4
                About.Text = " In the Shortest Remaining Time First (SRTF) scheduling algorithm, the process with the smallest amount of
time remaining until completion is selected to execute. Since the currently executing process is the one with the shortest amount of time
remaining by definition, and since that time should only reduce as execution progresses, processes will always run until they complete or
a new process is added that requires a smaller amount of time. "
                DataGridView2.Rows.Clear()

                If prioritiesArray.Count > 0 Then
                    Dim iMessage As DialogResult = MessageBox.Show("Process priorities will be discarded in this scheduling algorythm, click Yes to proceed or No to return to Priority Scheduling", "DataGridView System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If iMessage = DialogResult.Yes Then
                        prioritiesArray.Clear()


                        If DataGridView1.Columns.Count > 3 Then
                            DataGridView1.Columns.RemoveAt(3)
                            DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                        End If


                        TextBox4.Width = 10
                        TextBox4.Visible = False
                        Button1.Location = New Point(345, 169)
                        TextBox1.Select()

                        ComboBox2.Items.Clear()
                        ComboBox2.Items.AddRange(source1.ToArray)
                        GroupBox2.Visible = True
                        Panel2.Visible = False
                        ComboBox2.SelectedIndex = 2
                        ComboBox2.Enabled = True
                    Else
                        ComboBox1.SelectedIndex = 3
                    End If
                Else

                    If DataGridView1.Columns.Count > 3 Then
                        DataGridView1.Columns.RemoveAt(3)
                        DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                    End If


                    TextBox4.Width = 10
                    TextBox4.Visible = False
                    Button1.Location = New Point(345, 169)
                    TextBox1.Select()

                    ComboBox2.Items.Clear()
                    ComboBox2.Items.AddRange(source1.ToArray)
                    GroupBox2.Visible = True
                    Panel2.Visible = False
                    ComboBox2.SelectedIndex = 2
                    ComboBox2.Enabled = True
                End If

            Case 5
                About.Text = " The name of this algorithm comes from the round-robin principle, where each person gets an equal share of 
something in turns. It is the oldest, simplest scheduling algorithm, which is mostly used for multitasking.

In Round-robin scheduling, each ready task runs turn by turn only in a cyclic queue for a limited time slice. This algorithm also offers
starvation free execution of processes. "

                DataGridView2.Rows.Clear()

                If prioritiesArray.Count > 0 Then
                    Dim iMessage As DialogResult = MessageBox.Show("Process priorities will be discarded in this scheduling algorythm, click Yes to proceed or No to return to Priority Scheduling", "DataGridView System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If iMessage = DialogResult.Yes Then
                        prioritiesArray.Clear()
                        ComboBox2.Items.Clear()
                        ComboBox2.Items.AddRange(source1.ToArray)
                        ComboBox2.SelectedIndex = 2
                        ComboBox2.Enabled = False
                        GroupBox2.Visible = True
                        Panel2.Visible = False

                        If DataGridView1.Columns.Count > 3 Then
                            DataGridView1.Columns.RemoveAt(3)
                            DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                        End If

                        TextBox4.Width = 10
                        TextBox4.Visible = False
                        Button1.Location = New Point(345, 169)
                        TextBox1.Select()
                    Else
                        ComboBox1.SelectedIndex = 3
                    End If
                Else
                    ComboBox2.Items.Clear()
                    ComboBox2.Items.AddRange(source1.ToArray)
                    ComboBox2.SelectedIndex = 2
                    ComboBox2.Enabled = False
                    GroupBox2.Visible = True
                    Panel2.Visible = False

                    If DataGridView1.Columns.Count > 3 Then
                        DataGridView1.Columns.RemoveAt(3)
                        DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                    End If

                    TextBox4.Width = 10
                    TextBox4.Visible = False
                    Button1.Location = New Point(345, 169)
                    TextBox1.Select()
                End If

                ' Case 6
                '    About.Text = " It may happen that processes in the ready queue can be divided into different classes where each class has
                'its own scheduling needs. For example, a common division is a foreground (interactive) process and a background (batch) process. These two
                'classes have different scheduling needs. For this kind of situation Multilevel Queue Scheduling is used. 

                'Now, let us see how it works."
                'If DataGridView1.Columns.Count > 3 Then
                'DataGridView1.Columns.RemoveAt(3)
                'End If

                ' TextBox4.Width = 10
                ' TextBox4.Visible = False
                ' Button1.Location = New Point(345, 169)
                'TextBox1.Select()
            Case Else
                About.Text = " Select a sampling method to use. "

                If DataGridView1.Columns.Count > 3 Then
                    DataGridView1.Columns.RemoveAt(3)
                    DataGridView1.Columns.Item(2).HeaderText = "Arrival Time"
                End If


                TextBox4.Width = 10
                TextBox4.Visible = False
                Button1.Location = New Point(345, 169)
        End Select

    End Sub




    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub



    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs)
    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub


    Private Sub TextBox_KeyPressId(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        Select Case Char.IsLetterOrDigit(e.KeyChar)
            Case True
                e.Handled = False
            Case Else
                Select Case Char.IsControl(e.KeyChar)
                    Case True
                        e.Handled = False
                    Case Else
                        e.Handled = True
                End Select
        End Select
    End Sub

    Private Sub TextBox_KeyDownId(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub













    'main

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clearAll()

        Select Case ComboBox1.SelectedIndex
            Case 1

                FCFS_Non_Preemptive()

            Case 2

                SJF_Non_Preemptive()



            Case 5


                If Integer.TryParse(InputBox("Specify Quantum Time                                                                  * Decimals and Integers less than 1 are not accepted"), quantumTime) And quantumTime > 0 Then


                    Round_Robin_Preemptive()


                Else
                    MsgBox("Quantum Time is required for this type of squeduling")
                End If

            Case 3
                Select Case ComboBox2.SelectedIndex
                    Case 0
                        ComboBox2.SelectedIndex = 1
                    Case 1
                        Priority_Non_Preemtive()
                    Case 2
                        Priority_Preemtive()
                End Select

            Case 4
                SJF_Preemptive()
            Case Else
                MsgBox("Choose a sampling method to use")

        End Select
    End Sub


    Private Function clearAll()
        clearedIndexes.Clear()
        processesArray.Clear()

        startTimeArray.Clear()
        startTime_stopArray.Clear()
        stopTimeArray.Clear()
        complitionTime.Clear()
        turnAroundTime.Clear()
        waitingTime.Clear()
        responseTime.Clear()
    End Function



    'FCFS Non Preemptive

    Private Function FCFS_Non_Preemptive()
        Dim multiTask, minArrival, minArrivalIndex As Double
        Dim minVal As Double = -1
        Dim multitaskIndexes As New ArrayList()
        Dim readyBurstTimes As New ArrayList()
        Dim readyIndexes As New ArrayList()

        Dim status As Boolean = True
        Dim index As Integer = 0
        Dim minValIndex As Integer = -1
        Dim cpuStatus As Integer = 0

        clearAll()


        While status

            ' check if task arrive
            Dim num1 As Integer = 0
            If arrivalTimeArray.Contains(index.ToString) Then
                For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                    If Integer.Parse(arrivalTimeArray.Item(subIndex)) = index Then
                        If num1 < 1 Then
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            num1 += 1
                        Else
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))

                        End If
                        ' MsgBox("contains arrival " + index.ToString + " index " + Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)).ToString)
                    End If
                Next
            End If

            ' set cpu status
            If cpuStatus = 0 Then
                'cpu idle
                If readyIndexes.Count < 1 Then
                    ' no ready indexes
                    ' set idle
                    If stopTimeArray.Count < 1 Then
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Integer.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                If num1 < 1 Then
                                    minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    num1 += 1
                                Else
                                    If minArrival > Integer.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    End If

                                End If



                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    If minArrival.ToString.Length < 1 Then
                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + "...")
                                        'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                        status = False
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    Else

                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + minArrival.ToString)
                                        stopTimeArray.Add(minArrival)
                                        index = minArrival - 1
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    End If



                                End If
                            End If
                        Next

                        minArrival = -1

                    Else
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Double.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                processesArray.Add("Idle")
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + Double.Parse(arrivalTimeArray.Item(subIndex)).ToString)
                                stopTimeArray.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                subIndex = Integer.Parse(arrivalTimeArray.Count - 1)
                                complitionTime.Add(" - ")
                                turnAroundTime.Add(" - ")
                                waitingTime.Add(" - ")
                                responseTime.Add(" - ")
                            Else
                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    processesArray.Add("Idle")
                                    startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + "...")
                                    'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                    status = False
                                    'MsgBox("stopped by out of task")
                                    complitionTime.Add(" - ")
                                    turnAroundTime.Add(" - ")
                                    waitingTime.Add(" - ")
                                    responseTime.Add(" - ")
                                End If
                            End If
                        Next


                    End If

                Else
                    ' ready indexes available

                    ' assign task to cpu

                    If stopTimeArray.Count < 1 Then
                        processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                        startTime_stopArray.Add("0" + " - " + burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))).ToString)
                        stopTimeArray.Add(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))))
                        complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                        turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                        waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                        responseTime.Add("0")
                    Else
                        processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                        startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))))).ToString)
                        stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))))).ToString)
                        complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                        turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                        waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                        responseTime.Add(stopTimeArray.Item(stopTimeArray.Count - 2))
                    End If
                    clearedIndexes.Add(Integer.Parse(readyIndexes.Item(0)))
                    readyIndexes.RemoveAt(0)
                    cpuStatus = 1



                End If
            Else
                ' cpu busy

                If index = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) - 1 Then
                    cpuStatus = 0
                End If
            End If

            If stopTimeArray.Count > 0 And index + 1 = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) Then
                cpuStatus = 0

            End If

            If processIdArray.Count = clearedIndexes.Count Then
                status = False
                'MsgBox("stopped by cleared Indexes")

            End If


            multiTask = 0
            minValIndex = -1
            minVal = -1
            index += 1

        End While
        DataGridView2.Rows.Clear()

        For subIndex As Integer = 0 To processesArray.Count - 1
            DataGridView2.Rows.Add(processesArray.Item(subIndex), startTime_stopArray.Item(subIndex), complitionTime.Item(subIndex), turnAroundTime.Item(subIndex), waitingTime.Item(subIndex), responseTime.Item(subIndex))
        Next

        DataGridView2.Visible = True


        Dim buffer As Double = 0
        Dim buffer1 As Double = 0
        Dim temp As Double
        For subIndex As Integer = 0 To turnAroundTime.Count - 1
            If (Double.TryParse(turnAroundTime.Item(subIndex).ToString, temp)) Then
                buffer += Double.Parse(turnAroundTime.Item(subIndex).ToString)
                buffer1 += Double.Parse(waitingTime.Item(subIndex).ToString)
            End If

        Next

        Label4.Text = (buffer / processIdArray.Count).ToString
        Label5.Text = (buffer1 / processIdArray.Count).ToString
        Panel2.Visible = True
    End Function

    'SJF Non Preemptive

    Private Function SJF_Non_Preemptive()
        clearAll()

        Dim multiTask, minArrival, minArrivalIndex As Double
        Dim minVal As Double = -1
        Dim multitaskIndexes As New ArrayList()
        Dim readyBurstTimes As New ArrayList()
        Dim readyIndexes As New ArrayList()
        Dim status As Boolean = True
        Dim index As Integer = 0
        Dim minValIndex As Integer = -1
        Dim cpuStatus As Integer = 0


        'Process Burst Time	Arrival Time
        'P1          6 ms	    2 ms
        'P2          2 ms	    5 ms
        'P3          8 ms	    1 ms
        'P4          3 ms	    0 ms
        'P5          4 ms	    4 ms



        While status

            ' check if task arrive
            Dim num1 As Integer = 0
            If arrivalTimeArray.Contains(index.ToString) Then
                For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                    If Integer.Parse(arrivalTimeArray.Item(subIndex)) = index Then
                        If num1 < 1 Then
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            num1 += 1
                        Else
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))

                        End If
                        ' MsgBox("contains arrival " + index.ToString + " index " + Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)).ToString)
                    End If
                Next
            End If

            multitaskIndexes.Clear()
            num1 = 0

            ' set cpu status
            If cpuStatus = 0 Then
                'cpu idle
                If readyIndexes.Count < 1 Then
                    ' no ready indexes

                    ' set idle
                    ' MsgBox("setting idle at index = " + index.ToString)
                    If stopTimeArray.Count < 1 Then
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Integer.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                If num1 < 1 Then
                                    minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    num1 += 1
                                Else
                                    If minArrival > Integer.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    End If

                                End If



                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    If minArrival.ToString.Length < 1 Then
                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + "...")
                                        'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                        status = False
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    Else

                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + minArrival.ToString)
                                        stopTimeArray.Add(minArrival)
                                        index = minArrival - 1
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    End If



                                End If
                            End If
                        Next

                        minArrival = -1

                    Else
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Double.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                processesArray.Add("Idle")
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + Double.Parse(arrivalTimeArray.Item(subIndex)).ToString)
                                stopTimeArray.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                subIndex = Integer.Parse(arrivalTimeArray.Count - 1)
                                complitionTime.Add(" - ")
                                turnAroundTime.Add(" - ")
                                waitingTime.Add(" - ")
                                responseTime.Add(" - ")
                            Else
                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    processesArray.Add("Idle")
                                    startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + "...")
                                    'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                    status = False
                                    'MsgBox("stopped by out of task")
                                    complitionTime.Add(" - ")
                                    turnAroundTime.Add(" - ")
                                    waitingTime.Add(" - ")
                                    responseTime.Add(" - ")
                                End If
                            End If
                        Next


                    End If



                Else
                    ' ready task available
                    ' find min burst time

                    'If index = 7 Then
                    'MsgBox("readyIndexes =" + readyIndexes.Count.ToString)
                    'End If

                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If subIndex = 0 Then
                            minVal = burstTimeArray.Item(readyIndexes.Item(subIndex))
                        Else
                            If minVal > Double.Parse(burstTimeArray.Item(readyIndexes.Item(subIndex))) Then
                                minVal = burstTimeArray.Item(readyIndexes.Item(subIndex))
                            End If
                        End If

                    Next
                    ' find qty of min burst
                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If minVal = Double.Parse(burstTimeArray.Item(readyIndexes.Item(subIndex))) Then
                            multiTask += 1
                            minValIndex = Double.Parse(readyIndexes.Item(subIndex))
                        End If
                    Next


                    If multiTask = 1 Then
                        'assign task to cpu
                        'MsgBox("1 minval")
                        ' MsgBox(minVal.ToString + " " + minValIndex.ToString + " " + multiTask.ToString)
                        If stopTimeArray.Count < 1 Then
                            processesArray.Add(processIdArray.Item(minValIndex))
                            startTime_stopArray.Add("0" + " - " + minVal.ToString)
                            stopTimeArray.Add(minVal)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            responseTime.Add("0")
                        Else
                            processesArray.Add(processIdArray.Item(minValIndex))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + minVal).ToString)
                            stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + minVal).ToString)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            responseTime.Add(stopTimeArray.Item(stopTimeArray.Count - 2))
                        End If
                        clearedIndexes.Add(minValIndex)
                        readyIndexes.Remove(minValIndex)
                        cpuStatus = 1
                    Else
                        'MsgBox("more than 1")
                        Dim num As Integer = 0
                        ' For subIndex As Integer = 0 To readyIndexes.Count - 1
                        'MsgBox("index =" + readyIndexes.Item(subIndex).ToString)
                        ' Next
                        For subIndex As Integer = 0 To burstTimeArray.Count - 1
                            'MsgBox("selected minVal=" + minVal.ToString + " current burst=" + Double.Parse(burstTimeArray.Item((subIndex))).ToString + " contained in readyIndexes=" + readyIndexes.Contains(subIndex).ToString + " subIndex=" + subIndex.ToString)


                            If minVal = Double.Parse(burstTimeArray.Item(subIndex)) And readyIndexes.Contains(subIndex) Then
                                'MsgBox("True")
                                If num < 1 Then
                                    minArrival = arrivalTimeArray.Item(subIndex)
                                    minArrivalIndex = subIndex
                                    num += 1
                                Else
                                    If minArrival > Double.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Double.Parse(arrivalTimeArray.Item(subIndex))
                                        'MsgBox("min arrival =" + minArrival.ToString)
                                        minArrivalIndex = subIndex
                                        'MsgBox("True +")
                                    End If
                                End If
                            End If


                        Next
                        'MsgBox("curent minval =" + minVal.ToString + "selected minval index =" + minArrivalIndex.ToString + " minval qty =" + multiTask.ToString + " num=" + num.ToString)

                        ' assign task to cpu

                        If stopTimeArray.Count < 1 Then
                            processesArray.Add(processIdArray.Item(minArrivalIndex))
                            startTime_stopArray.Add("0" + " - " + minVal.ToString)
                            stopTimeArray.Add(minVal)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            responseTime.Add("0")
                        Else
                            processesArray.Add(processIdArray.Item(minArrivalIndex))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + minVal).ToString)
                            stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + minVal).ToString)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            responseTime.Add(stopTimeArray.Item(stopTimeArray.Count - 2))
                        End If
                        clearedIndexes.Add(minArrivalIndex)
                        readyIndexes.Remove(Integer.Parse(minArrivalIndex))
                        cpuStatus = 1

                    End If

                End If



            Else
                'cpu busy


                If index = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) - 1 Then
                    cpuStatus = 0
                End If

            End If

            If stopTimeArray.Count > 0 And index + 1 = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) Then
                cpuStatus = 0

            End If

            If processIdArray.Count = clearedIndexes.Count Then
                status = False
                'MsgBox("stopped by cleared Indexes")

            End If


            multiTask = 0
            minValIndex = -1
            minVal = -1
            index += 1
        End While

        'MsgBox("Final index = " + index.ToString + " output = " + processesArray.Count.ToString + " " + startTime_stopArray.Count.ToString)

        DataGridView2.Rows.Clear()

        For subIndex As Integer = 0 To processesArray.Count - 1
            DataGridView2.Rows.Add(processesArray.Item(subIndex), startTime_stopArray.Item(subIndex), complitionTime.Item(subIndex), turnAroundTime.Item(subIndex), waitingTime.Item(subIndex), responseTime.Item(subIndex))
        Next

        DataGridView2.Visible = True

        Dim buffer As Double = 0
        Dim buffer1 As Double = 0
        Dim temp As Double
        For subIndex As Integer = 0 To turnAroundTime.Count - 1
            If (Double.TryParse(turnAroundTime.Item(subIndex).ToString, temp)) Then
                buffer += Double.Parse(turnAroundTime.Item(subIndex).ToString)
                buffer1 += Double.Parse(waitingTime.Item(subIndex).ToString)
            End If

        Next

        Label4.Text = (buffer / processIdArray.Count).ToString
        Label5.Text = (buffer1 / processIdArray.Count).ToString

        Panel2.Visible = True
    End Function

    'SJF Preemptive
    Private Function SJF_Preemptive()
        clearAll()

        Dim multiTask, minArrival, minArrivalIndex As Double
        Dim minVal As Double = -1
        Dim multitaskIndexes As New ArrayList()
        Dim burstTimes As New ArrayList()
        Dim readyIndexes As New ArrayList()

        Dim status As Boolean = True
        Dim index As Integer = 0
        Dim minValIndex As Integer = -1
        Dim currentTaskIndex As Integer
        Dim currentBurst As Integer
        Dim cpuStatus As Integer = 0

        burstTimes = burstTimeArray.Clone
        'MsgBox("Burst count" + burstTimes.Count.ToString)
        While status

            ' check if task arrive
            Dim num1 As Integer = 0
            If arrivalTimeArray.Contains(index.ToString) Then
                For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                    If Integer.Parse(arrivalTimeArray.Item(subIndex)) = index Then
                        If num1 < 1 Then
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))

                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            num1 += 1
                        Else
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))
                            'readyburstTimes.Add(burstTimeArray.Item(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1)))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))

                        End If
                        ' MsgBox("contains arrival " + index.ToString + " index " + Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)).ToString)
                    End If
                Next
            End If

            multitaskIndexes.Clear()
            num1 = 0


            ' set cpu status
            If cpuStatus = 0 Then
                'cpu idle
                If readyIndexes.Count < 1 Then
                    ' no ready indexes

                    ' set idle
                    ' MsgBox("setting idle at index = " + index.ToString)
                    If stopTimeArray.Count < 1 Then
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Integer.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                If num1 < 1 Then
                                    minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    num1 += 1
                                Else
                                    If minArrival > Integer.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    End If

                                End If



                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    If minArrival.ToString.Length < 1 Then
                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + "...")
                                        'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                        status = False
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    Else

                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + minArrival.ToString)
                                        stopTimeArray.Add(minArrival)
                                        index = minArrival - 1
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    End If

                                End If
                            End If
                        Next

                        minArrival = -1

                    Else
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Double.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                processesArray.Add("Idle")
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + Double.Parse(arrivalTimeArray.Item(subIndex)).ToString)
                                stopTimeArray.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                subIndex = Integer.Parse(arrivalTimeArray.Count - 1)
                                complitionTime.Add(" - ")
                                turnAroundTime.Add(" - ")
                                waitingTime.Add(" - ")
                                responseTime.Add(" - ")
                            Else
                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    processesArray.Add("Idle")
                                    startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + "...")
                                    'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                    status = False
                                    'MsgBox("stopped by out of task")
                                    complitionTime.Add(" - ")
                                    turnAroundTime.Add(" - ")
                                    waitingTime.Add(" - ")
                                    responseTime.Add(" - ")
                                End If
                            End If
                        Next


                    End If

                Else
                    ' ready task available
                    ' find min burst 

                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If subIndex = 0 Then
                            minVal = burstTimes.Item(readyIndexes.Item(subIndex))
                        Else
                            If minVal > Double.Parse(burstTimes.Item(readyIndexes.Item(subIndex))) Then
                                minVal = burstTimes.Item(readyIndexes.Item(subIndex))
                            End If
                        End If

                    Next

                    ' find qty of min burst
                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If minVal = Double.Parse(burstTimes.Item(readyIndexes.Item(subIndex))) Then
                            multiTask += 1
                            minValIndex = Double.Parse(readyIndexes.Item(subIndex))
                        End If
                    Next



                    'assign cpu to task
                    If multiTask = 1 Then

                        If stopTimeArray.Count < 1 Then
                            processesArray.Add(processIdArray.Item(minValIndex))
                            startTime_stopArray.Add("0" + " - " + burstTimes.Item(minValIndex).ToString)
                            stopTimeArray.Add(burstTimes.Item(minValIndex))

                        Else
                            processesArray.Add(processIdArray.Item(minValIndex))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minValIndex))).ToString)
                            stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minValIndex))).ToString)

                        End If



                        If Double.Parse(burstTimes.Item(minValIndex)) = 1 Then
                            clearedIndexes.Add(minValIndex)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(minValIndex))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(minValIndex)))
                            responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(minValIndex)).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(minValIndex)).ToString).ToString.IndexOf("-")))
                        Else
                            cpuStatus = 1


                        End If



                        readyIndexes.Remove(minValIndex)
                        currentTaskIndex = minValIndex
                    Else
                        'MsgBox("more than 1")
                        Dim num As Integer = 0

                        For subIndex As Integer = 0 To burstTimeArray.Count - 1
                            'MsgBox("selected minVal=" + minVal.ToString + " current burst=" + Double.Parse(burstTimeArray.Item((subIndex))).ToString + " contained in readyIndexes=" + readyIndexes.Contains(subIndex).ToString + " subIndex=" + subIndex.ToString)


                            If minVal = Double.Parse(burstTimes.Item(subIndex)) And readyIndexes.Contains(subIndex) Then
                                'MsgBox("True")
                                If num < 1 Then
                                    minArrival = arrivalTimeArray.Item(subIndex)
                                    minArrivalIndex = subIndex
                                    num += 1
                                Else
                                    If minArrival > Double.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Double.Parse(arrivalTimeArray.Item(subIndex))
                                        'MsgBox("min arrival =" + minArrival.ToString)
                                        minArrivalIndex = subIndex
                                        'MsgBox("True +")
                                    End If
                                End If
                            End If


                        Next
                        If stopTimeArray.Count < 1 Then
                            processesArray.Add(processIdArray.Item(minArrivalIndex))
                            startTime_stopArray.Add("0" + " - " + burstTimes.Item(minArrivalIndex).ToString)
                            stopTimeArray.Add(burstTimes.Item(minArrivalIndex))

                        Else
                            processesArray.Add(processIdArray.Item(minArrivalIndex))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minArrivalIndex))).ToString)
                            stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minArrivalIndex))).ToString)

                        End If

                        If Double.Parse(burstTimes.Item(minValIndex)) = 1 Then
                            clearedIndexes.Add(minValIndex)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(minValIndex))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(minValIndex)))
                            responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(minValIndex)).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(minValIndex)).ToString).ToString.IndexOf("-")))


                        Else
                            cpuStatus = 1

                        End If

                        'MsgBox(minArrivalIndex.ToString)
                        readyIndexes.Remove(Integer.Parse(minArrivalIndex))
                        currentTaskIndex = minArrivalIndex
                    End If

                End If



            Else
                'cpu busy
                'MsgBox("Busy " + index.ToString)
                If arrivalTimeArray.Contains(index.ToString) Then
                    readyIndexes.Add(currentTaskIndex)

                    'set remaining burst for currentRunning task
                    burstTimes.Item(currentTaskIndex) = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) - index


                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If subIndex = 0 Then
                            minVal = burstTimes.Item(readyIndexes.Item(subIndex))
                        Else
                            If minVal > Double.Parse(burstTimes.Item(readyIndexes.Item(subIndex))) Then
                                minVal = burstTimes.Item(readyIndexes.Item(subIndex))
                            End If
                        End If

                    Next

                    ' find qty of min priority
                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If minVal = Double.Parse(burstTimes.Item(readyIndexes.Item(subIndex))) Then
                            multiTask += 1
                            minValIndex = Double.Parse(readyIndexes.Item(subIndex))
                        End If
                    Next



                    If minValIndex = currentTaskIndex Then

                        readyIndexes.Remove(currentTaskIndex)
                    Else

                        If multiTask = 1 Then

                            startTime_stopArray.Item(startTime_stopArray.Count - 1) = startTime_stopArray.Item(startTime_stopArray.Count - 1).ToString.Remove(startTime_stopArray.Item(startTime_stopArray.Count - 1).ToString.IndexOf("-") + 1).ToString + index.ToString

                            burstTimes.RemoveAt(currentTaskIndex)
                            burstTimes.Insert(currentTaskIndex, Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) - index)


                            stopTimeArray.RemoveAt(stopTimeArray.Count - 1)
                            stopTimeArray.Add(index)


                            If stopTimeArray.Count < 1 Then
                                processesArray.Add(processIdArray.Item(minValIndex))
                                startTime_stopArray.Add("0" + " - " + burstTimes.Item(minValIndex).ToString)
                                stopTimeArray.Add(burstTimes.Item(minValIndex))

                            Else

                                processesArray.Add(processIdArray.Item(minValIndex))
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minValIndex))).ToString)
                                stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minValIndex))).ToString)

                            End If

                            cpuStatus = 1
                            readyIndexes.Remove(minValIndex)
                            currentTaskIndex = minValIndex

                        Else

                            Dim num As Integer = 0

                            For subIndex As Integer = 0 To burstTimeArray.Count - 1


                                If minVal = Double.Parse(burstTimes.Item(subIndex)) And readyIndexes.Contains(subIndex) Then
                                    'MsgBox("True")
                                    If num < 1 Then
                                        minArrival = arrivalTimeArray.Item(subIndex)
                                        minArrivalIndex = subIndex
                                        num += 1
                                    Else
                                        If minArrival > Double.Parse(arrivalTimeArray.Item(subIndex)) Then
                                            minArrival = Double.Parse(arrivalTimeArray.Item(subIndex))

                                            minArrivalIndex = subIndex

                                        End If
                                    End If
                                End If


                            Next
                            ' resetting preemted tasks duration and stop time

                            startTime_stopArray.Item(startTime_stopArray.Count - 1) = startTime_stopArray.Item(startTime_stopArray.Count - 1).ToString.Remove(startTime_stopArray.Item(startTime_stopArray.Count - 1).ToString.IndexOf("-") + 1).ToString + index.ToString

                            burstTimes.RemoveAt(currentTaskIndex)
                            burstTimes.Insert(currentTaskIndex, Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) - index)


                            stopTimeArray.RemoveAt(stopTimeArray.Count - 1)
                            stopTimeArray.Add(index)


                            ' assign task to cpu
                            If stopTimeArray.Count < 1 Then
                                processesArray.Add(processIdArray.Item(minArrivalIndex))
                                startTime_stopArray.Add("0" + " - " + burstTimeArray.Item(minArrivalIndex).ToString)
                                stopTimeArray.Add(burstTimeArray.Item(minArrivalIndex))

                            Else
                                processesArray.Add(processIdArray.Item(minArrivalIndex))
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(minArrivalIndex))).ToString)
                                stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(minArrivalIndex))).ToString)

                            End If

                            cpuStatus = 1
                            readyIndexes.Remove(minArrivalIndex)
                            currentTaskIndex = minArrivalIndex
                        End If
                    End If
                Else

                End If
                If stopTimeArray.Count > 0 And index + 1 = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) Then
                    cpuStatus = 0
                    clearedIndexes.Add(currentTaskIndex)
                    complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                    turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(currentTaskIndex)))
                    waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(currentTaskIndex)))
                    responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(currentTaskIndex)).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(currentTaskIndex)).ToString).ToString.IndexOf("-")))
                Else

                End If
            End If



            If processIdArray.Count = clearedIndexes.Count Then
                status = False

            End If


            multiTask = 0
            minValIndex = -1
            minVal = -1
            index += 1
        End While


        DataGridView2.Rows.Clear()

        'MsgBox(processesArray.Count.ToString + " " + startTime_stopArray.Count.ToString + " " + complitionTime.Count.ToString + " " + turnAroundTime.Count.ToString + " " + waitingTime.Count.ToString + " " + responseTime.Count.ToString + " ")

        For subIndex As Integer = 0 To processesArray.Count - 1
            DataGridView2.Rows.Add(processesArray.Item(subIndex), startTime_stopArray.Item(subIndex), complitionTime.Item(subIndex), turnAroundTime.Item(subIndex), waitingTime.Item(subIndex), responseTime.Item(subIndex))
        Next
        DataGridView2.Visible = True

        Dim buffer As Double = 0
        Dim buffer1 As Double = 0
        Dim temp As Double
        For subIndex As Integer = 0 To turnAroundTime.Count - 1
            If (Double.TryParse(turnAroundTime.Item(subIndex).ToString, temp)) Then
                buffer += Double.Parse(turnAroundTime.Item(subIndex).ToString)
                buffer1 += Double.Parse(waitingTime.Item(subIndex).ToString)
            End If

        Next

        Label4.Text = (buffer / processIdArray.Count).ToString
        Label5.Text = (buffer1 / processIdArray.Count).ToString

        Panel2.Visible = True
    End Function

    'Round Robin 
    Private Function Round_Robin_Preemptive()
        clearAll()

        Dim multiTask, minArrival, minArrivalIndex As Double
        Dim minVal As Double = -1
        Dim multitaskIndexes As New ArrayList()
        Dim readyBurstTimes As New ArrayList()
        Dim readyIndexes As New ArrayList()
        Dim status As Boolean = True
        Dim index As Integer = 0
        Dim minValIndex As Integer = -1
        Dim cpuStatus As Integer = 0
        Dim robinStat As Boolean = False
        'quanrum Time 


        While status
            'MsgBox("index " + index.ToString)
            ' check if task arrive
            Dim num1 As Integer = 0
            If arrivalTimeArray.Contains(index.ToString) Then
                For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                    If Integer.Parse(arrivalTimeArray.Item(subIndex)) = index Then
                        If num1 < 1 Then
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            readyBurstTimes.Add(burstTimeArray.Item(arrivalTimeArray.IndexOf(index.ToString)))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            num1 += 1
                        Else
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))
                            readyBurstTimes.Add(burstTimeArray.Item(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1)))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))

                        End If
                        ' MsgBox("contains arrival " + index.ToString + " index " + Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)).ToString)
                    End If
                Next
            End If



            If robinStat Then
                readyIndexes.Add(readyIndexes.Item(0))
                readyBurstTimes.Add(Integer.Parse(readyBurstTimes.Item(0)) - quantumTime)
                readyIndexes.RemoveAt(0)
                readyBurstTimes.RemoveAt(0)

                robinStat = False
            End If

            'MsgBox("Ready indexes = " + readyIndexes.Count.ToString)

            'For subIndex As Integer = 0 To readyIndexes.Count - 1
            ' MsgBox(readyIndexes.Item(subIndex).ToString + " burst= " + readyBurstTimes.Item(subIndex).ToString)
            ' Next

            ' If stopTimeArray.Count > 0 Then
            'If index = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) Then
            'If robinStat Then
            'readyIndexes.Add((readyIndexes.Item(0)))
            'readyBurstTimes.Add(Integer.Parse(readyBurstTimes.Item(0)) - quantumTime)
            'End If
            'readyIndexes.RemoveAt(0)
            'readyBurstTimes.RemoveAt(0)

            ' End If

            ' End If



            'MsgBox(readyBurstTimes.Item(0).ToString)
            ' set cpu status
            If cpuStatus = 0 Then
                'cpu idle
                If readyIndexes.Count < 1 Then
                    ' no ready indexes
                    ' set idle
                    If stopTimeArray.Count < 1 Then
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Integer.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                If num1 < 1 Then
                                    minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    num1 += 1
                                Else
                                    If minArrival > Integer.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    End If

                                End If



                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    If minArrival.ToString.Length < 1 Then
                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + "...")
                                        'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                        status = False
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    Else

                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + minArrival.ToString)
                                        stopTimeArray.Add(minArrival)
                                        index = minArrival - 1
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    End If



                                End If
                            End If
                        Next

                        minArrival = -1

                    Else
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Double.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                processesArray.Add("Idle")
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + Double.Parse(arrivalTimeArray.Item(subIndex)).ToString)
                                stopTimeArray.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                subIndex = Integer.Parse(arrivalTimeArray.Count - 1)
                                complitionTime.Add(" - ")
                                turnAroundTime.Add(" - ")
                                waitingTime.Add(" - ")
                                responseTime.Add(" - ")
                            Else
                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    processesArray.Add("Idle")
                                    startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + "...")
                                    'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                    status = False
                                    complitionTime.Add(" - ")
                                    turnAroundTime.Add(" - ")
                                    waitingTime.Add(" - ")
                                    responseTime.Add(" - ")
                                End If
                            End If
                        Next


                    End If

                Else
                    ' ready indexes available

                    ' assign task to cpu



                    If stopTimeArray.Count < 1 Then

                        If readyIndexes.Count = 1 And processIdArray.Count - clearedIndexes.Count = 1 Then

                            'MsgBox(Double.Parse(readyBurstTimes.Item(0)).ToString + " " + quantumTime.ToString + " " + index.ToString)
                            processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                            startTime_stopArray.Add("0" + " - " + Double.Parse(readyBurstTimes.Item(0)).ToString)
                            stopTimeArray.Add(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))))

                            clearedIndexes.Add(Integer.Parse(readyIndexes.Item(0)))
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(readyIndexes.Item(0))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(readyIndexes.Item(0))))
                            responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.IndexOf("-")))


                            If Double.Parse(readyBurstTimes.Item(0)) = 1 Or quantumTime = 1 Then
                                ' clearedIndexes.Add(minValIndex)
                            Else
                                cpuStatus = 1
                            End If
                            readyIndexes.RemoveAt(0)
                            readyBurstTimes.RemoveAt(0)
                            robinStat = False

                            ' MsgBox("only one ready burst remaining : Index cleared " + clearedIndexes.Count.ToString)
                        Else

                            If Double.Parse(readyBurstTimes.Item(0)) < quantumTime Then
                                'MsgBox(Double.Parse(readyBurstTimes.Item(0)).ToString + " " + quantumTime.ToString + " " + index.ToString)
                                processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                                startTime_stopArray.Add("0" + " - " + burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))).ToString)
                                stopTimeArray.Add(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))))

                                clearedIndexes.Add(Integer.Parse(readyIndexes.Item(0)))
                                complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                                turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(readyIndexes.Item(0))))
                                waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(readyIndexes.Item(0))))
                                responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.IndexOf("-")))


                                If Double.Parse(readyBurstTimes.Item(0)) = 1 Or quantumTime = 1 Then
                                    ' clearedIndexes.Add(minValIndex)
                                    If Double.Parse(readyBurstTimes.Item(0)) = 1 Then
                                        readyIndexes.RemoveAt(0)
                                        readyBurstTimes.RemoveAt(0)
                                    End If
                                Else
                                    cpuStatus = 1
                                End If

                                robinStat = False

                                'MsgBox("Burst < quantum : Index cleared " + clearedIndexes.Count.ToString)
                            ElseIf Integer.Parse(readyBurstTimes.Item(0)) > quantumTime Then
                                processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                                startTime_stopArray.Add("0" + " - " + quantumTime.ToString)
                                stopTimeArray.Add(quantumTime)
                                complitionTime.Add(" - ")
                                turnAroundTime.Add(" - ")
                                waitingTime.Add(" - ")
                                responseTime.Add(" - ")


                                If Double.Parse(readyBurstTimes.Item(0)) = 1 Or quantumTime = 1 Then
                                    ' clearedIndexes.Add(minValIndex)
                                    robinStat = True
                                Else
                                    cpuStatus = 1

                                End If


                                'MsgBox("Burst > quantum")
                            ElseIf Integer.Parse(readyBurstTimes.Item(0)) = quantumTime Then
                                processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                                startTime_stopArray.Add("0" + " - " + quantumTime.ToString)
                                stopTimeArray.Add(quantumTime)
                                clearedIndexes.Add(Integer.Parse(readyIndexes.Item(0)))
                                complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                                turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(readyIndexes.Item(0))))
                                waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(readyIndexes.Item(0))))
                                responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.IndexOf("-")))



                                If Double.Parse(readyBurstTimes.Item(0)) = 1 Or quantumTime = 1 Then
                                    ' clearedIndexes.Add(minValIndex)
                                    robinStat = True
                                Else
                                    cpuStatus = 1
                                End If

                                robinStat = False

                                'MsgBox("Burst = quantum : Index cleared " + clearedIndexes.Count.ToString)
                            End If

                        End If
                    Else
                        'processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                        'startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))))).ToString)
                        'stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0))))).ToString)

                        If readyIndexes.Count = 1 And processIdArray.Count - clearedIndexes.Count = 1 Then

                            'MsgBox(Double.Parse(readyBurstTimes.Item(0)).ToString + " " + quantumTime.ToString + " " + index.ToString)
                            processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(readyBurstTimes.Item(0))).ToString)
                            stopTimeArray.Add(Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(readyBurstTimes.Item(0)))

                            clearedIndexes.Add(Integer.Parse(readyIndexes.Item(0)))
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(readyIndexes.Item(0))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(readyIndexes.Item(0))))
                            responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.IndexOf("-")))


                            If Double.Parse(readyBurstTimes.Item(0)) = 1 Or quantumTime = 1 Then
                                ' clearedIndexes.Add(minValIndex)
                            Else
                                cpuStatus = 1
                            End If
                            readyIndexes.RemoveAt(0)
                            readyBurstTimes.RemoveAt(0)
                            robinStat = False

                            'MsgBox("only one ready burst remaining : Index cleared " + clearedIndexes.Count.ToString)
                        Else

                            If Integer.Parse(readyBurstTimes.Item(0)) < quantumTime Then
                                processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(readyBurstTimes.Item(0))).ToString)
                                stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(readyBurstTimes.Item(0))))

                                clearedIndexes.Add(Integer.Parse(readyIndexes.Item(0)))
                                complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                                turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(readyIndexes.Item(0))))
                                waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(readyIndexes.Item(0))))
                                responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.IndexOf("-")))


                                If Double.Parse(readyBurstTimes.Item(0)) = 1 Or quantumTime = 1 Then
                                    ' clearedIndexes.Add(minValIndex)
                                    If Double.Parse(readyBurstTimes.Item(0)) = 1 Then
                                        readyIndexes.RemoveAt(0)
                                        readyBurstTimes.RemoveAt(0)
                                    End If
                                Else
                                    cpuStatus = 1
                                End If

                                robinStat = False

                                '   MsgBox("Burst < quantum : Index cleared " + clearedIndexes.Count.ToString)
                            ElseIf Integer.Parse(readyBurstTimes.Item(0)) > quantumTime Then
                                processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + quantumTime).ToString)
                                stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + quantumTime))

                                complitionTime.Add(" - ")
                                turnAroundTime.Add(" - ")
                                waitingTime.Add(" - ")
                                responseTime.Add(" - ")

                                If Double.Parse(readyBurstTimes.Item(0)) = 1 Or quantumTime = 1 Then
                                    ' clearedIndexes.Add(minValIndex)
                                    robinStat = True
                                Else
                                    cpuStatus = 1
                                End If



                                '  MsgBox("Burst > quantum")
                            ElseIf Integer.Parse(readyBurstTimes.Item(0)) = quantumTime Then
                                processesArray.Add(processIdArray.Item(Integer.Parse(readyIndexes.Item(0))))
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + quantumTime).ToString)
                                stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + quantumTime).ToString)
                                clearedIndexes.Add(Integer.Parse(readyIndexes.Item(0)))
                                complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                                turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(readyIndexes.Item(0))))
                                waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(readyIndexes.Item(0))))
                                responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.IndexOf("-")))


                                If Double.Parse(readyBurstTimes.Item(0)) = 1 Or quantumTime = 1 Then
                                    ' clearedIndexes.Add(minValIndex)

                                Else
                                    cpuStatus = 1
                                End If

                                robinStat = False

                                ' MsgBox("Burst = quantum : Index cleared " + clearedIndexes.Count.ToString)
                            End If
                        End If
                    End If


                End If

            Else
                ' cpu busy
                'MsgBox("Busy " + index.ToString)


                If stopTimeArray.Count > 0 And index + 1 = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) Then
                    If Double.Parse(readyBurstTimes.Item(0)) <= quantumTime Then
                        'clearedIndexes.Add(readyIndexes.Item(0))
                        'complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                        'turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(readyIndexes.Item(0))))
                        'waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(readyIndexes.Item(0))))
                        'responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(readyIndexes.Item(0))).ToString).ToString.IndexOf("-")))

                        'readyIndexes.RemoveAt(0)
                        'readyBurstTimes.RemoveAt(0)
                        readyIndexes.RemoveAt(0)
                        readyBurstTimes.RemoveAt(0)
                    Else

                        robinStat = True
                    End If
                    cpuStatus = 0

                End If
            End If

            If processIdArray.Count = clearedIndexes.Count Then
                status = False
                'MsgBox("stopped by cleared indexes")
            End If


            multiTask = 0
            minValIndex = -1
            minVal = -1
            index += 1

        End While
        DataGridView2.Rows.Clear()

        ' MsgBox(processesArray.Count.ToString + " " + startTime_stopArray.Count.ToString + " " + complitionTime.Count.ToString + " " + turnAroundTime.Count.ToString + " " + waitingTime.Count.ToString + " " + responseTime.Count.ToString + " ")

        For subIndex As Integer = 0 To processesArray.Count - 1
            DataGridView2.Rows.Add(processesArray.Item(subIndex), startTime_stopArray.Item(subIndex), complitionTime.Item(subIndex), turnAroundTime.Item(subIndex), waitingTime.Item(subIndex), responseTime.Item(subIndex))
        Next

        DataGridView2.Visible = True

        Dim buffer As Double = 0
        Dim buffer1 As Double = 0
        Dim temp As Double
        For subIndex As Integer = 0 To turnAroundTime.Count - 1
            If (Double.TryParse(turnAroundTime.Item(subIndex).ToString, temp)) Then
                buffer += Double.Parse(turnAroundTime.Item(subIndex).ToString)
                buffer1 += Double.Parse(waitingTime.Item(subIndex).ToString)
            End If

        Next

        Label4.Text = (buffer / processIdArray.Count).ToString
        Label5.Text = (buffer1 / processIdArray.Count).ToString
        Panel2.Visible = True


    End Function

    'Priority Preemptive
    Private Function Priority_Preemtive()
        clearAll()

        Dim multiTask, minArrival, minArrivalIndex As Double
        Dim minVal As Double = -1
        Dim multitaskIndexes As New ArrayList()
        Dim burstTimes As New ArrayList()
        Dim readyIndexes As New ArrayList()
        Dim status As Boolean = True
        Dim index As Integer = 0
        Dim minValIndex As Integer = -1
        Dim currentTaskIndex As Integer
        Dim currentBurst As Integer
        Dim cpuStatus As Integer = 0

        burstTimes = burstTimeArray.Clone
        'MsgBox("Burst count" + burstTimes.Count.ToString)
        While status

            ' check if task arrive
            Dim num1 As Integer = 0
            If arrivalTimeArray.Contains(index.ToString) Then
                For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                    If Integer.Parse(arrivalTimeArray.Item(subIndex)) = index Then
                        If num1 < 1 Then
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))

                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            num1 += 1
                        Else
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))
                            'readyburstTimes.Add(burstTimeArray.Item(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1)))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))

                        End If
                        ' MsgBox("contains arrival " + index.ToString + " index " + Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)).ToString)
                    End If
                Next
            End If

            multitaskIndexes.Clear()
            num1 = 0


            ' set cpu status
            If cpuStatus = 0 Then
                'cpu idle
                If readyIndexes.Count < 1 Then
                    ' no ready indexes

                    ' set idle
                    ' MsgBox("setting idle at index = " + index.ToString)
                    If stopTimeArray.Count < 1 Then
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Integer.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                If num1 < 1 Then
                                    minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    num1 += 1
                                Else
                                    If minArrival > Integer.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    End If

                                End If



                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    If minArrival.ToString.Length < 1 Then
                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + "...")
                                        'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                        status = False
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    Else

                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + minArrival.ToString)
                                        stopTimeArray.Add(minArrival)
                                        index = minArrival - 1
                                        complitionTime.Add(" - ")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    End If

                                End If
                            End If
                        Next

                        minArrival = -1

                    Else
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Double.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                processesArray.Add("Idle")
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + Double.Parse(arrivalTimeArray.Item(subIndex)).ToString)
                                stopTimeArray.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                subIndex = Integer.Parse(arrivalTimeArray.Count - 1)
                                complitionTime.Add(" - ")
                                turnAroundTime.Add(" - ")
                                waitingTime.Add(" - ")
                                responseTime.Add(" - ")
                            Else
                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    processesArray.Add("Idle")
                                    startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + "...")
                                    'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                    status = False
                                    complitionTime.Add(" - ")
                                    turnAroundTime.Add(" - ")
                                    waitingTime.Add(" - ")
                                    responseTime.Add(" - ")
                                    'MsgBox("stopped by out of task")
                                End If
                            End If
                        Next


                    End If

                Else
                    ' ready task available
                    ' find min priority 

                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If subIndex = 0 Then
                            minVal = prioritiesArray.Item(readyIndexes.Item(subIndex))
                        Else
                            If minVal > Double.Parse(prioritiesArray.Item(readyIndexes.Item(subIndex))) Then
                                minVal = prioritiesArray.Item(readyIndexes.Item(subIndex))
                            End If
                        End If

                    Next

                    ' find qty of min priority
                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If minVal = Double.Parse(prioritiesArray.Item(readyIndexes.Item(subIndex))) Then
                            multiTask += 1
                            minValIndex = Double.Parse(readyIndexes.Item(subIndex))
                        End If
                    Next



                    'assign cpu to task
                    If multiTask = 1 Then

                        If stopTimeArray.Count < 1 Then
                            processesArray.Add(processIdArray.Item(minValIndex))
                            startTime_stopArray.Add("0" + " - " + burstTimes.Item(minValIndex).ToString)
                            stopTimeArray.Add(burstTimes.Item(minValIndex))


                        Else
                            processesArray.Add(processIdArray.Item(minValIndex))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minValIndex))).ToString)
                            stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minValIndex))).ToString)


                        End If

                        If Double.Parse(burstTimes.Item(minValIndex)) = 1 Then
                            clearedIndexes.Add(minValIndex)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(minValIndex))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(minValIndex)))
                            responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(minValIndex)).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(minValIndex)).ToString).ToString.IndexOf("-")))


                        Else
                            cpuStatus = 1

                        End If

                        readyIndexes.Remove(minValIndex)
                        currentTaskIndex = minValIndex
                    Else
                        'MsgBox("more than 1")
                        Dim num As Integer = 0

                        For subIndex As Integer = 0 To burstTimeArray.Count - 1
                            'MsgBox("selected minVal=" + minVal.ToString + " current burst=" + Double.Parse(burstTimeArray.Item((subIndex))).ToString + " contained in readyIndexes=" + readyIndexes.Contains(subIndex).ToString + " subIndex=" + subIndex.ToString)


                            If minVal = Double.Parse(prioritiesArray.Item(subIndex)) And readyIndexes.Contains(subIndex) Then
                                'MsgBox("True")
                                If num < 1 Then
                                    minArrival = arrivalTimeArray.Item(subIndex)
                                    minArrivalIndex = subIndex
                                    num += 1
                                Else
                                    If minArrival > Double.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Double.Parse(arrivalTimeArray.Item(subIndex))
                                        'MsgBox("min arrival =" + minArrival.ToString)
                                        minArrivalIndex = subIndex
                                        'MsgBox("True +")
                                    End If
                                End If
                            End If


                        Next
                        If stopTimeArray.Count < 1 Then
                            processesArray.Add(processIdArray.Item(minArrivalIndex))
                            startTime_stopArray.Add("0" + " - " + burstTimes.Item(minArrivalIndex).ToString)
                            stopTimeArray.Add(burstTimes.Item(minArrivalIndex))

                        Else
                            processesArray.Add(processIdArray.Item(minArrivalIndex))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minArrivalIndex))).ToString)
                            stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minArrivalIndex))).ToString)

                        End If

                        If Double.Parse(burstTimes.Item(minArrivalIndex)) = 1 Then
                            clearedIndexes.Add(minValIndex)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(minArrivalIndex)))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(minArrivalIndex)))
                            responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(minArrivalIndex)).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(minArrivalIndex)).ToString).ToString.IndexOf("-")))

                        Else
                            cpuStatus = 1

                        End If

                        'MsgBox(minArrivalIndex.ToString)
                        readyIndexes.Remove(Integer.Parse(minArrivalIndex))
                        currentTaskIndex = minArrivalIndex
                    End If

                End If



            Else
                'cpu busy
                'MsgBox("Busy " + index.ToString)
                If arrivalTimeArray.Contains(index.ToString) Then
                    readyIndexes.Add(currentTaskIndex)
                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If subIndex = 0 Then
                            minVal = prioritiesArray.Item(readyIndexes.Item(subIndex))
                        Else
                            If minVal > Double.Parse(prioritiesArray.Item(readyIndexes.Item(subIndex))) Then
                                minVal = prioritiesArray.Item(readyIndexes.Item(subIndex))
                            End If
                        End If

                    Next

                    ' find qty of min priority
                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If minVal = Double.Parse(prioritiesArray.Item(readyIndexes.Item(subIndex))) Then
                            multiTask += 1
                            minValIndex = Double.Parse(readyIndexes.Item(subIndex))
                        End If
                    Next
                    'MsgBox(minVal.ToString)


                    'assign cpu to task
                    If minValIndex = currentTaskIndex Then
                        'MsgBox("same")
                        readyIndexes.Remove(currentTaskIndex)
                    Else
                        'MsgBox("different")
                        If multiTask = 1 Then
                            ' resetting preemted tasks duration and stop time

                            startTime_stopArray.Item(startTime_stopArray.Count - 1) = startTime_stopArray.Item(startTime_stopArray.Count - 1).ToString.Remove(startTime_stopArray.Item(startTime_stopArray.Count - 1).ToString.IndexOf("-") + 1).ToString + index.ToString

                            burstTimes.RemoveAt(currentTaskIndex)
                            burstTimes.Insert(currentTaskIndex, Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) - index)

                            'MsgBox("Remaining burst" + burstTimes.Item(currentTaskIndex).ToString)

                            stopTimeArray.RemoveAt(stopTimeArray.Count - 1)
                            stopTimeArray.Add(index)
                            'MsgBox("new stop time " + stopTimeArray.Item(stopTimeArray.Count - 1).ToString)


                            If stopTimeArray.Count < 1 Then
                                processesArray.Add(processIdArray.Item(minValIndex))
                                startTime_stopArray.Add("0" + " - " + burstTimes.Item(minValIndex).ToString)
                                stopTimeArray.Add(burstTimes.Item(minValIndex))

                            Else
                                'MsgBox("Stop more than 1")
                                processesArray.Add(processIdArray.Item(minValIndex))
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minValIndex))).ToString)
                                stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimes.Item(minValIndex))).ToString)

                            End If

                            cpuStatus = 1
                            readyIndexes.Remove(minValIndex)
                            currentTaskIndex = minValIndex

                        Else

                            Dim num As Integer = 0

                            For subIndex As Integer = 0 To burstTimeArray.Count - 1
                                'MsgBox("selected minVal=" + minVal.ToString + " current burst=" + Double.Parse(burstTimeArray.Item((subIndex))).ToString + " contained in readyIndexes=" + readyIndexes.Contains(subIndex).ToString + " subIndex=" + subIndex.ToString)


                                If minVal = Double.Parse(prioritiesArray.Item(subIndex)) And readyIndexes.Contains(subIndex) Then
                                    'MsgBox("True")
                                    If num < 1 Then
                                        minArrival = arrivalTimeArray.Item(subIndex)
                                        minArrivalIndex = subIndex
                                        num += 1
                                    Else
                                        If minArrival > Double.Parse(arrivalTimeArray.Item(subIndex)) Then
                                            minArrival = Double.Parse(arrivalTimeArray.Item(subIndex))
                                            'MsgBox("min arrival =" + minArrival.ToString)
                                            minArrivalIndex = subIndex
                                            'MsgBox("True +")
                                        End If
                                    End If
                                End If


                            Next
                            ' resetting preemted tasks duration and stop time

                            startTime_stopArray.Item(startTime_stopArray.Count - 1) = startTime_stopArray.Item(startTime_stopArray.Count - 1).ToString.Remove(startTime_stopArray.Item(startTime_stopArray.Count - 1).ToString.IndexOf("-") + 1).ToString + index.ToString

                            burstTimes.RemoveAt(currentTaskIndex)
                            burstTimes.Insert(currentTaskIndex, Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) - index)

                            'MsgBox("Remaining burst" + burstTimes.Item(currentTaskIndex).ToString)

                            stopTimeArray.RemoveAt(stopTimeArray.Count - 1)
                            stopTimeArray.Add(index)
                            'MsgBox("new stop time " + stopTimeArray.Item(stopTimeArray.Count - 1).ToString)


                            ' assign task to cpu
                            If stopTimeArray.Count < 1 Then
                                processesArray.Add(processIdArray.Item(minArrivalIndex))
                                startTime_stopArray.Add("0" + " - " + burstTimeArray.Item(minArrivalIndex).ToString)
                                stopTimeArray.Add(burstTimeArray.Item(minArrivalIndex))

                            Else
                                processesArray.Add(processIdArray.Item(minArrivalIndex))
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(minArrivalIndex))).ToString)
                                stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(minArrivalIndex))).ToString)

                            End If

                            cpuStatus = 1
                            readyIndexes.Remove(minArrivalIndex)
                            currentTaskIndex = minArrivalIndex
                        End If
                    End If
                Else
                    '   MsgBox("no arrival in busy")
                End If
                If stopTimeArray.Count > 0 And index + 1 = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) Then
                    cpuStatus = 0
                    clearedIndexes.Add(currentTaskIndex)
                    complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))
                    turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(currentTaskIndex)))
                    waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(currentTaskIndex)))
                    responseTime.Add(startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(currentTaskIndex)).ToString).ToString.Substring(0, startTime_stopArray.Item(processesArray.IndexOf(processIdArray.Item(currentTaskIndex)).ToString).ToString.IndexOf("-")))
                Else

                End If
            End If



            If processIdArray.Count = clearedIndexes.Count Then
                status = False

            End If


            multiTask = 0
            minValIndex = -1
            minVal = -1
            index += 1
        End While

        'MsgBox("Final index = " + index.ToString + " output = " + processesArray.Count.ToString + " " + startTime_stopArray.Count.ToString)

        DataGridView2.Rows.Clear()

        MsgBox(processesArray.Count.ToString + " " + startTime_stopArray.Count.ToString + " " + complitionTime.Count.ToString + " " + turnAroundTime.Count.ToString + " " + waitingTime.Count.ToString + " " + responseTime.Count.ToString + " ")

        For subIndex As Integer = 0 To processesArray.Count - 1
            DataGridView2.Rows.Add(processesArray.Item(subIndex), startTime_stopArray.Item(subIndex), complitionTime.Item(subIndex), turnAroundTime.Item(subIndex), waitingTime.Item(subIndex), responseTime.Item(subIndex))
        Next

        DataGridView2.Visible = True

        Dim buffer As Double = 0
        Dim buffer1 As Double = 0
        Dim temp As Double
        For subIndex As Integer = 0 To turnAroundTime.Count - 1
            If (Double.TryParse(turnAroundTime.Item(subIndex).ToString, temp)) Then
                buffer += Double.Parse(turnAroundTime.Item(subIndex).ToString)
                buffer1 += Double.Parse(waitingTime.Item(subIndex).ToString)
            End If

        Next

        Label4.Text = (buffer / processIdArray.Count).ToString
        Label5.Text = (buffer1 / processIdArray.Count).ToString

        Panel2.Visible = True
    End Function

    'Priority Non Preemptive
    Private Function Priority_Non_Preemtive()
        clearAll()

        Dim multiTask, minArrival, minArrivalIndex As Double
        Dim minVal As Double = -1
        Dim multitaskIndexes As New ArrayList()
        Dim readyBurstTimes As New ArrayList()
        Dim readyIndexes As New ArrayList()

        Dim status As Boolean = True
        Dim index As Integer = 0
        Dim minValIndex As Integer = -1
        Dim cpuStatus As Integer = 0

        While status

            ' check if task arrive
            Dim num1 As Integer = 0
            If arrivalTimeArray.Contains(index.ToString) Then
                For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                    If Integer.Parse(arrivalTimeArray.Item(subIndex)) = index Then
                        If num1 < 1 Then
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString))
                            num1 += 1
                        Else
                            readyIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))
                            multitaskIndexes.Add(arrivalTimeArray.IndexOf(index.ToString, Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)) + 1))

                        End If
                        ' MsgBox("contains arrival " + index.ToString + " index " + Integer.Parse(multitaskIndexes.Item(multitaskIndexes.Count - 1)).ToString)
                    End If
                Next
            End If

            multitaskIndexes.Clear()
            num1 = 0

            ' set cpu status
            If cpuStatus = 0 Then
                'cpu idle
                If readyIndexes.Count < 1 Then
                    ' no ready indexes

                    ' set idle
                    ' MsgBox("setting idle at index = " + index.ToString)
                    If stopTimeArray.Count < 1 Then
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Integer.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                If num1 < 1 Then
                                    minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    num1 += 1
                                Else
                                    If minArrival > Integer.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Integer.Parse(arrivalTimeArray.Item(subIndex))
                                    End If

                                End If



                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    If minArrival.ToString.Length < 1 Then
                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + "...")
                                        'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                        status = False
                                        complitionTime.Add("-")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    Else

                                        processesArray.Add("Idle")
                                        startTime_stopArray.Add("0" + " - " + minArrival.ToString)
                                        stopTimeArray.Add(minArrival)
                                        index = minArrival - 1
                                        complitionTime.Add("-")
                                        turnAroundTime.Add(" - ")
                                        waitingTime.Add(" - ")
                                        responseTime.Add(" - ")
                                    End If



                                End If
                            End If
                        Next

                        minArrival = -1

                    Else
                        For subIndex As Integer = 0 To arrivalTimeArray.Count - 1
                            If Double.Parse(arrivalTimeArray.Item(subIndex)) > index Then
                                processesArray.Add("Idle")
                                startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + Double.Parse(arrivalTimeArray.Item(subIndex)).ToString)
                                stopTimeArray.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                subIndex = Integer.Parse(arrivalTimeArray.Count - 1)
                                complitionTime.Add(" - ")
                                turnAroundTime.Add(" - ")
                                waitingTime.Add(" - ")
                                responseTime.Add(" - ")
                            Else
                                If subIndex = Integer.Parse(arrivalTimeArray.Count - 1) Then
                                    processesArray.Add("Idle")
                                    startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + "...")
                                    'stopTimeArray_SJN.Add(Double.Parse(arrivalTimeArray.Item(subIndex)))
                                    status = False
                                    'MsgBox("stopped by out of task")
                                    complitionTime.Add(" - ")
                                    turnAroundTime.Add(" - ")
                                    waitingTime.Add(" - ")
                                    responseTime.Add(" - ")
                                End If
                            End If
                        Next


                    End If



                Else
                    ' ready task available
                    ' find min priority 



                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If subIndex = 0 Then
                            minVal = prioritiesArray.Item(readyIndexes.Item(subIndex))
                        Else
                            If minVal > Double.Parse(prioritiesArray.Item(readyIndexes.Item(subIndex))) Then
                                minVal = prioritiesArray.Item(readyIndexes.Item(subIndex))
                            End If
                        End If

                    Next

                    ' find qty of min priority
                    For subIndex As Integer = 0 To readyIndexes.Count - 1
                        If minVal = Double.Parse(prioritiesArray.Item(readyIndexes.Item(subIndex))) Then
                            multiTask += 1
                            minValIndex = Double.Parse(readyIndexes.Item(subIndex))
                        End If
                    Next


                    If multiTask = 1 Then
                        'assign task to cpu
                        'MsgBox("1 minval")
                        ' MsgBox(minVal.ToString + " " + minValIndex.ToString + " " + multiTask.ToString)
                        If stopTimeArray.Count < 1 Then
                            processesArray.Add(processIdArray.Item(minValIndex))
                            startTime_stopArray.Add("0" + " - " + burstTimeArray.Item(minValIndex).ToString)
                            stopTimeArray.Add(burstTimeArray.Item(minValIndex))
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))

                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            responseTime.Add(stopTimeArray.Item(stopTimeArray.Count - 2))
                        Else
                            processesArray.Add(processIdArray.Item(minValIndex))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(minValIndex))).ToString)
                            stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(minValIndex))).ToString)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))

                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            responseTime.Add(stopTimeArray.Item(stopTimeArray.Count - 2))
                        End If
                        clearedIndexes.Add(minValIndex)
                        readyIndexes.Remove(minValIndex)
                        cpuStatus = 1
                    Else
                        'MsgBox("more than 1")
                        Dim num As Integer = 0
                        ' For subIndex As Integer = 0 To readyIndexes.Count - 1
                        'MsgBox("index =" + readyIndexes.Item(subIndex).ToString)
                        ' Next
                        For subIndex As Integer = 0 To burstTimeArray.Count - 1
                            'MsgBox("selected minVal=" + minVal.ToString + " current burst=" + Double.Parse(burstTimeArray.Item((subIndex))).ToString + " contained in readyIndexes=" + readyIndexes.Contains(subIndex).ToString + " subIndex=" + subIndex.ToString)


                            If minVal = Double.Parse(prioritiesArray.Item(subIndex)) And readyIndexes.Contains(subIndex) Then
                                'MsgBox("True")
                                If num < 1 Then
                                    minArrival = arrivalTimeArray.Item(subIndex)
                                    minArrivalIndex = subIndex
                                    num += 1
                                Else
                                    If minArrival > Double.Parse(arrivalTimeArray.Item(subIndex)) Then
                                        minArrival = Double.Parse(arrivalTimeArray.Item(subIndex))
                                        'MsgBox("min arrival =" + minArrival.ToString)
                                        minArrivalIndex = subIndex
                                        'MsgBox("True +")
                                    End If
                                End If
                            End If


                        Next
                        'MsgBox("curent minval =" + minVal.ToString + "selected minval index =" + minArrivalIndex.ToString + " minval qty =" + multiTask.ToString + " num=" + num.ToString)

                        ' assign task to cpu

                        If stopTimeArray.Count < 1 Then
                            processesArray.Add(processIdArray.Item(minArrivalIndex))
                            startTime_stopArray.Add("0" + " - " + burstTimeArray.Item(minArrivalIndex).ToString)
                            stopTimeArray.Add(burstTimeArray.Item(minArrivalIndex))
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))

                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            responseTime.Add(stopTimeArray.Item(stopTimeArray.Count - 2))
                        Else
                            processesArray.Add(processIdArray.Item(minArrivalIndex))
                            startTime_stopArray.Add(stopTimeArray.Item(stopTimeArray.Count - 1).ToString + " - " + (Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(minArrivalIndex))).ToString)
                            stopTimeArray.Add((Double.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) + Double.Parse(burstTimeArray.Item(minArrivalIndex))).ToString)
                            complitionTime.Add(stopTimeArray.Item(stopTimeArray.Count - 1))

                            turnAroundTime.Add(Integer.Parse(complitionTime.Item(complitionTime.Count - 1)) - Integer.Parse(arrivalTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            waitingTime.Add(Integer.Parse(turnAroundTime.Item(turnAroundTime.Count - 1)) - Integer.Parse(burstTimeArray.Item(Integer.Parse(readyIndexes.Item(0)))))
                            responseTime.Add(stopTimeArray.Item(stopTimeArray.Count - 2))
                        End If
                        clearedIndexes.Add(minArrivalIndex)
                        readyIndexes.Remove(Integer.Parse(minArrivalIndex))
                        cpuStatus = 1

                    End If

                End If



            Else
                'cpu busy


                If index = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) - 1 Then
                    cpuStatus = 0
                End If

            End If

            If stopTimeArray.Count > 0 And index + 1 = Integer.Parse(stopTimeArray.Item(stopTimeArray.Count - 1)) Then
                cpuStatus = 0

            End If

            If processIdArray.Count = clearedIndexes.Count Then
                status = False
                'MsgBox("stopped by cleared Indexes")

            End If


            multiTask = 0
            minValIndex = -1
            minVal = -1
            index += 1
        End While

        'MsgBox("Final index = " + index.ToString + " output = " + processesArray.Count.ToString + " " + startTime_stopArray.Count.ToString)

        DataGridView2.Rows.Clear()

        For subIndex As Integer = 0 To processesArray.Count - 1
            DataGridView2.Rows.Add(processesArray.Item(subIndex), startTime_stopArray.Item(subIndex), complitionTime.Item(subIndex), turnAroundTime.Item(subIndex), waitingTime.Item(subIndex), responseTime.Item(subIndex))
        Next

        DataGridView2.Visible = True

        Dim buffer As Double = 0
        Dim buffer1 As Double = 0
        Dim temp As Double
        For subIndex As Integer = 0 To turnAroundTime.Count - 1
            If (Double.TryParse(turnAroundTime.Item(subIndex).ToString, temp)) Then
                buffer += Double.Parse(turnAroundTime.Item(subIndex).ToString)
                buffer1 += Double.Parse(waitingTime.Item(subIndex).ToString)
            End If

        Next

        Label4.Text = (buffer / processIdArray.Count).ToString
        Label5.Text = (buffer1 / processIdArray.Count).ToString

        Panel2.Visible = True
    End Function


    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Select Case e.KeyChar.ToString
            Case "."
                Select Case DataGridView1.CurrentCell.EditedFormattedValue.ToString.Contains(".")
                    Case True
                        MsgBox("Double decimal not accepted")
                        e.Handled = True
                    Case Else

                        e.Handled = False

                End Select
            Case 0
                e.Handled = False
            Case 1
                e.Handled = False
            Case 2
                e.Handled = False
            Case 3
                e.Handled = False
            Case 4
                e.Handled = False
            Case 5
                e.Handled = False
            Case 6
                e.Handled = False
            Case 7
                e.Handled = False
            Case 8
                e.Handled = False
            Case 9
                e.Handled = False
            Case Else
                Select Case Char.IsControl(e.KeyChar)
                    Case True
                        e.Handled = False

                    Case Else
                        e.Handled = True

                End Select
        End Select

    End Sub



    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.BackColor = Color.White
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress

        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            TextBox2.Select()
        End If

    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        TextBox4.BackColor = Color.White

        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Button1.Select()
        End If

    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress

        If TextBox4.Visible Then
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                TextBox4.Select()
            End If
        Else

            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                Button1.Select()
            End If
        End If


        Select Case e.KeyChar.ToString
            Case "."
                Select Case TextBox3.Text.ToString.Contains(".")
                    Case True
                        MsgBox("Double decimal not accepted")
                        e.Handled = True
                    Case Else

                        e.Handled = False

                End Select
            Case 0
                e.Handled = False
            Case 1
                e.Handled = False
            Case 2
                e.Handled = False
            Case 3
                e.Handled = False
            Case 4
                e.Handled = False
            Case 5
                e.Handled = False
            Case 6
                e.Handled = False
            Case 7
                e.Handled = False
            Case 8
                e.Handled = False
            Case 9
                e.Handled = False
            Case Else
                Select Case Char.IsControl(e.KeyChar)
                    Case True
                        e.Handled = False

                    Case Else
                        e.Handled = True

                End Select
        End Select

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            TextBox3.Select()
        End If
        Select Case e.KeyChar.ToString
            Case "."
                Select Case TextBox2.Text.ToString.Contains(".")
                    Case True
                        MsgBox("Double decimal not accepted")
                        e.Handled = True
                    Case Else

                        e.Handled = False

                End Select
            Case 0
                e.Handled = False
            Case 1
                e.Handled = False
            Case 2
                e.Handled = False
            Case 3
                e.Handled = False
            Case 4
                e.Handled = False
            Case 5
                e.Handled = False
            Case 6
                e.Handled = False
            Case 7
                e.Handled = False
            Case 8
                e.Handled = False
            Case 9
                e.Handled = False
            Case Else
                Select Case Char.IsControl(e.KeyChar)
                    Case True
                        e.Handled = False

                    Case Else
                        e.Handled = True

                End Select
        End Select

    End Sub

    Private Sub selected_Cell(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        If DataGridView1.CurrentRow.Selected = True Then
            Button4.Enabled = True
        Else
            Button4.Enabled = False
        End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text.Length < 1 Or TextBox2.Text.Length < 1 Or TextBox3.Text.Length < 1 Then
            If TextBox1.Text.Length < 1 Then
                TextBox1.BackColor = Color.OrangeRed
            End If
            If TextBox2.Text.Length < 1 Then
                TextBox2.BackColor = Color.OrangeRed
            End If
            If TextBox3.Text.Length < 1 Then
                TextBox3.BackColor = Color.OrangeRed
            End If
            If TextBox4.Text.Length < 1 And TextBox4.Visible Then
                TextBox4.BackColor = Color.OrangeRed
            End If

        Else
            processIdArray.Add(TextBox1.Text.ToString)
            burstTimeArray.Add(TextBox2.Text.ToString)
            arrivalTimeArray.Add(TextBox3.Text.ToString)
            If TextBox4.Visible Then
                prioritiesArray.Add(TextBox4.Text.ToString)
            End If


            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""

            Button2.Enabled = True
            refreshData()
        End If
        TextBox1.Select()
    End Sub

    Private Function refreshData()

        DataGridView1.Rows.Clear()
        'MsgBox(DataGridView1.Rows.Count)
        'Dim index As String

        If prioritiesArray.Count > 0 Then
            For index As Integer = 0 To processIdArray.Count - 1
                DataGridView1.Rows.Add(processIdArray.Item(index), burstTimeArray.Item(index), arrivalTimeArray.Item(index), prioritiesArray.Item(index))
            Next
        Else
            For index As Integer = 0 To processIdArray.Count - 1
                DataGridView1.Rows.Add(processIdArray.Item(index), burstTimeArray.Item(index), arrivalTimeArray.Item(index))
            Next
        End If
    End Function

    Private Sub TextBox2_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox2.BackColor = Color.White
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim clearAll As DialogResult
        clearAll = MessageBox.Show("Confirm if you want to clear all", "DataGridView System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If clearAll = DialogResult.Yes Then
            processIdArray.Clear()
            burstTimeArray.Clear()
            arrivalTimeArray.Clear()

            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            If ComboBox1.SelectedIndex = 3 Then
                prioritiesArray.Clear()
            End If
            Button2.Enabled = False
        End If

        DataGridView2.Visible = False

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim iMessage As DialogResult = MessageBox.Show("Confirm if you want to remove selected row", "DataGridView System", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If iMessage = DialogResult.Yes Then
            processIdArray.RemoveAt(DataGridView1.CurrentRow.Index)
            burstTimeArray.RemoveAt(DataGridView1.CurrentRow.Index)
            arrivalTimeArray.RemoveAt(DataGridView1.CurrentRow.Index)

            If ComboBox1.SelectedIndex = 3 Then
                prioritiesArray.RemoveAt(DataGridView1.CurrentRow.Index)
            End If


            If processIdArray.Count < 1 Then
                Button2.Enabled = False
            End If

            refreshData()
        End If


    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 2 And ComboBox2.SelectedIndex = 2 Then
            ComboBox1.SelectedIndex = 4
        ElseIf ComboBox1.SelectedIndex = 4 And ComboBox2.SelectedIndex = 1 Then
            ComboBox1.SelectedIndex = 2

        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        TextBox3.BackColor = Color.White
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class
