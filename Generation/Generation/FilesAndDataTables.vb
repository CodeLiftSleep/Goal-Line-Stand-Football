Imports System.IO
''' <summary>
''' Holds any Public variables for use within the Generation project
''' </summary>
Public Module FilesAndDataTables
    Public MT As New Mersenne.MersenneTwister
    Public CoachDT As New DataTable
    Public DraftDT As New DataTable
    Public PersonnelDT As New DataTable
    Public OwnerDT As New DataTable
    Public ScoutDT As New DataTable
    Public ScoutGradeDT As New DataTable
    Public PlayerDT As New DataTable
    Public FirstNames As New DataTable
    Public LastNames As New DataTable
    Public Colleges As New DataTable
    Public Eval As New Evaluation
    Public MyDB As String = "Football"
    Public SQLiteTables As New SQLFunctions.SQLiteDataFunctions
    Public MyCoach As New CoachType
    Public MyScoutAssignment As New ScoutAssignment
    Public MyPersonnel As New PersonnelType
    Public MyTrainer As New Trainers
    Dim filepath As String = "Project Files/"
    Public ReadFName As StreamReader = New StreamReader(filepath + "FName.txt")
    Public ReadLName As StreamReader = New StreamReader(filepath + "LName.txt")
    Public ReadCollege As StreamReader = New StreamReader(filepath + "Colleges.txt")
    ''' <summary>
    ''' Sets the assignment of the scout/presonnel person.  1-6 only scout players in their region---these are area scouts.  National scouts the entire nation of college players, selecting key individuals.  Advance scouts the next opponent.  Pro scouts the NFL players.
    ''' All scouts all of them---this would be like a GM, who is responsible for all players
    ''' </summary>
    Public Enum ScoutAssignment
        East = 1
        Atlantic = 2
        South = 3
        Midwest = 4
        Central = 5
        West = 6
        NationalCollege = 7
        Advance = 8
        Pro = 9
        All = 10
    End Enum
    ''' <summary>
    ''' Lists the types of coaches that can be on a staff.  Not all staffs will have every type of coach, although 85% of the staffs will be the same, some have fewer QC coaches than others, etc...
    ''' </summary>
    Public Enum CoachType
        HeadCoach = 1
        AssistantHeadCoach = 2
        OffensiveCoordinator = 3
        DefensiveCoordinator = 4
        SpecialTeamsCoach = 5
        AssistantSpecialTeamsCoach = 6
        QBCoach = 7
        RBCoach = 8
        WRCoach = 9
        AssistantWRCoach = 10
        TECoach = 11
        OLCoach = 12
        AssistantOLCoach = 13
        DLCoach = 14
        AssistantDLCoach = 15
        InsideLBCoach = 16
        OutsideLBCoach = 17
        DBCoach = 18
        AssistantDBCoach = 19
        OQualityControl = 20
        DQualityControl = 21
        StrCondCoach = 22
        AsstStrCondCoach = 23 '2-3 coaches
        OffensiveAssistant = 24
        DefensiveAssistant = 25
    End Enum

    ''' <summary>
    ''' Lists the type of Personnel the person is.  
    ''' </summary>
    Public Enum PersonnelType
        GM = 1
        AssistantGM = 2
        DirectorPlayerPersonnel = 3
        AssistantDirPlayerPersonnel = 4
        DirectorProPersonnel = 5
        AssistantDirProPersonnel = 6
        DirectorCollegeScouting = 7
        AssistantDirCollegeScouting = 8
        NationalCollegeScout = 9
        AreaScout = 10
        AdvanceScout = 11
        NatScoutingOrgScout = 12 'Will be BLESTO or NFS depending on what team they are on
        ScoutingAssistant = 13
    End Enum

    Public Enum Trainers
        HeadTrainer = 1
        AssistantTrainer = 2
        TeamPhysician = 3
        TeamOrthopedist = 4
        Physiotherapist = 5
    End Enum

End Module
