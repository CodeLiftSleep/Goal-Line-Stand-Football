
''' <summary>
''' This will be the parent class for both NFLPlayers and CollegePlayers.  
''' </summary>
Public Class Players
    Inherits Person

    ''' <summary>
    ''' FieldNames for players will use the same attributes, but the College Players will have additional Combine related fields as well.
    ''' </summary>
    ''' <param name="PlayerType"></param>
    ''' <returns></returns>
    Public Function GetSQLFields(ByVal PlayerType As String) As String
        Dim SQLFieldNames As String = ""
        Select Case PlayerType
            Case "College"

                SQLFieldNames = "DraftID int PRIMARY KEY NOT NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, ScoutRegion varchar(10) NULL, Age int NULL, DOB varchar(12) NULL, CollegePOS varchar(5) NULL, ActualGrade decimal(5,2) NULL, 
ProjNFLPos varchar(5) NULL, PosType varchar(20) NULL, Trait1 varchar(20) NULL, Trait2 varchar(20) NULL, Trait3 varchar(20), Height int NULL, Weight int NULL, ArmLength decimal(4,2) NULL, HandLength decimal(4,2) NULL, FortyYardTime decimal(3,2) NULL, 
TwentyYardTime decimal(3,2) NULL, TenYardTime decimal(3,2) NULL, ShortShuttle decimal(3,2) NULL, BroadJump int NULL, VertJump decimal(3,1) NULL, ThreeConeDrill decimal(3,2) NULL, BenchPress int NULL, InterviewSkills int NULL, WonderlicTest int NULL, 
SkillsTranslateToNFL int NULL, Reaction int NULL, QAB int NULL, COD int NULL, Hands int NULL, BodyCatch int NULL, ReleaseOffLine int NULL, CatchWhenHit int NULL, BreaksTackles int NULL, ContactBalance int NULL, RunAfterCatch int NULL, 
LowerBodyStrength int NULL, UpperBodyStrength int NULL, Footwork int NULL, HandUse int NULL, JumpingAbility int NULL, PassBlockVsPower int NULL, PassBlockVsSpeed int NULL, RunBlocking int NULL, PlaySpeed int NULL, RouteRunning int NULL, KickAccuracy int NULL, AdjustToBall int NULL, 
Tackling int NULL, Blitz int NULL, AvoidBlockers int NULL, ShedBlock int NULL, DefeatBlock int NULL, ManToManCoverage int NULL, ZoneCoverage int NULL, QBMechanics int NULL, QBRelQuickness int NULL, QBAccuracy int NULL, QBDecMaking int NULL, QBBallHandling int NULL, 
QBLocateRec int NULL, QBPocketPresence int NULL, QBEscape int NULL, QBScrambling int NULL, QBRolloutRight int NULL, QBRolloutLeft int NULL, QBArmStrength int NULL, QBTouch int NULL, QBPlayAction int NULL, RBRunVision int NULL, RBSetsUpBlocks int NULL, RBPatience int NULL, WRConcentration int NULL, 
WRRunDBOff int NULL, WRDisguiseRoute int NULL, WRRAC int NULL, TEConcentration int NULL, TEBallAdjust int NULL, TERAC int NULL,  OLPulling int NULL, OLSlide int NULL, OLMoveInSpace int NULL, 
OLSnapAbility int NULL, OLLongSnapAbility int NULL, OLAnchorAbility int NULL, OLRecover int NULL, DLPrimaryTech varchar(15) NULL, DLSecondaryTech varchar(15) NULL, DLRunAtHim int NULL, DLAgainstPullAbility int NULL, DLSlideABility int NULL, 
DLRunPursuit int NULL, DLPassRushTech varchar(15) NULL, DLCanTakeDoubleTeam int NULL, DLFinish int NULL, DLsetUpPassRush int NULL, LBDropDepth int NULL, LBFillGaps int NULL,  DBPressBailCoverage int NULL, DBRunContain int NULL,  
DBBump int NULL,  DBBaitQB int NULL, DBCatchUpSpeed int NULL, DBTechnique int NULL, KAccuracy int NULL, KFakeAbility int NULL, KKickRise int NULL, PFakeAbility int NULL, PDistance int NULL, PHangTime int NULL, STCoverage int NULL, STWillingness int NULL, STAssignment int NULL, STDiscipline int NULL, 
Flexibility int NULL, Clutch int NULL, Production int NULL, Consistency int NULL, TeamPlayer int NULL, Instincts int NULL, Focus int NULL, RETKickReturn int NULL, RETPuntReturn int NULL, PlayStrength int NULL, FilmStudy int NULL, Durability int NULL, 
Explosion int NULL, DeliversBlow int NULL, Leadership int NULL, Character int NULL, Toughness int NULL, WorkEthic int NULL, Aggressive int NULL, ReadKeys int NULL, FieldAwareness int NULL, PlaybookKnowledge int NULL, BallSecurity int NULL, 
LovesFootball int NULL, Concentration int NULL, Confidence int NULL, Coachability int NULL, Discipline int NULL, Maturity int NULL, HandlesElements int NULL, Potential int NULL, Raw int NULL"

            Case "NFL"

                SQLFieldNames = "PlayerID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, ScoutRegion varchar(10) NULL, Age int NULL, DOB varchar(12) NULL, Height int NULL, Weight int NULL, 
ArmLength decimal(4,2) NULL, HandLength decimal(4,2) NULL, Pos varchar(4) NULL, PosType varchar(20) NULL, Trait1 varchar(20) NULL, Trait2 varchar(20) NULL, Trait3 varchar(20), Reaction int NULL, QAB int NULL, COD int NULL, Hands int NULL, BodyCatch int NULL,
ReleaseOffLine int NULL, CatchWhenHit int NULL, BreaksTackles int NULL, ContactBalance int NULL, RunAfterCatch int NULL, LowerBodyStrength int NULL, UpperBodyStrength int NULL, Footwork int NULL, HandUse int NULL, JumpingAbility int NULL, PassBlockVsPower int NULL, PassBlockVsSpeed int NULL, 
RunBlocking int NULL, PlaySpeed int NULL, RouteRunning int NULL, KickAccuracy int NULL, AdjustToBall int NULL, Tackling int NULL, Blitz int NULL, AvoidBlockers int NULL, ShedBlock int NULL, DefeatBlock int NULL, ManToManCoverage int NULL, ZoneCoverage int NULL, 
QBMechanics int NULL, QBRelQuickness int NULL, QBAccuracy int NULL, QBDecMaking int NULL, QBLocateRec int NULL, QBPocketPresence int NULL, QBBallHandling int NULL, QBEscape int NULL, QBScrambling int NULL, QBRolloutRight int NULL, QBRolloutLeft int NULL, QBArmStrength int NULL, QBTouch int NULL, 
QBPlayAction int NULL, RBRunVision int NULL, RBSetsUpBlocks int NULL, RBPatience int NULL, WRConcentration int NULL, WRRunDBOff int NULL, WRDisguiseRoute int NULL, WRRAC int NULL, TEConcentration int NULL, TEBallAdjust int NULL, TERAC int NULL,
OLPulling int NULL, OLSlide int NULL, OLMoveInSpace int NULL, OLSnapAbility int NULL, OLLongSnapAbility int NULL, OLAnchorAbility int NULL, OLRecover int NULL, DLPrimaryTech varchar(15) NULL, 
DLSecondaryTech varchar(15) NULL, DLRunAtHim int NULL, DLAgainstPullAbility int NULL, DLSlideABility int NULL, DLRunPursuit int NULL, DLPassRushTechnique varchar(15) NULL,  DLCanTakeDoubleTeam int NULL, DLFinish int NULL, DLsetUpPassRush int NULL, 
LBDropDepth int NULL,  LBFillGaps int NULL, DBPressBailCoverage int NULL, DBRunContain int NULL, DBBump int NULL, DBBaitQB int NULL, DBCatchUpSpeed int NULL, DBTechnique int NULL, KAccuracy int NULL, KFakeAbility int NULL, KKickRise int NULL, PFakeAbility int NULL,
PDistance int NULL, PHangTime int NULL, STCoverage int NULL, STWillingness int NULL, STAssignment int NULL, STDiscipline int NULL, Flexibility int NULL, Clutch int NULL, Production int NULL, Consistency int NULL, TeamPlayer int NULL, Instincts int NULL, 
Focus int NULL, RETKickReturn int NULL, RETPuntReturn int NULL, PlayStrength int NULL, FilmStudy int NULL, Durability int NULL, Explosion int NULL, DeliversBlow int NULL, Leadership int NULL, Character int NULL, Toughness int NULL, WorkEthic int NULL, 
Aggressive int NULL, ReadKeys int NULL, FieldAwareness int NULL, PlaybookKnowledge int NULL, BallSecurity int NULL, LovesFootball int NULL, Concentration int NULL, Confidence int NULL, Coachability int NULL, Discipline int NULL, Maturity int NULL, HandlesElements int NULL, 
Potential int NULL, Raw int NULL"

        End Select
        Return SQLFieldNames
    End Function

    Public Function GetCollegePos() As String
        Select Case MT.GenerateDouble(0, 100)
            Case 0 To 5.186 : Return "QB"
            Case 5.187 To 12.102 : Return "RB"
            Case 12.103 To 14.209 : Return "FB"
            Case 14.21 To 24.743 : Return "WR"
            Case 24.744 To 30.956 : Return "TE"
            Case 30.957 To 35.764 : Return "C"
            Case 35.765 To 41.167 : Return "OG"
            Case 41.168 To 48.892 : Return "OT"
            Case 48.893 To 57.266 : Return "DE"
            Case 57.267 To 65.37 : Return "DT"
            Case 65.371 To 74.029 : Return "OLB"
            Case 74.03 To 78.23 : Return "ILB"
            Case 78.231 To 83.581 : Return "FS"
            Case 83.582 To 88.763 : Return "SS"
            Case 88.764 To 96.218 : Return "CB"
            Case 96.219 To 98.217 : Return "K"
            Case Else : Return "P"
        End Select
    End Function
    ''' <summary>
    ''' Gets the key ratings by running the exponential decay formula = RatingsStartPoint*((1-ratingsdecay)^TimeValue)+MT.generateint32(-10,10)
    ''' account for Draft Round and position of the player. Starting Point of the Ratings is the highest value---in this case 90
    ''' </summary>
    ''' <param name="DT"></param>
    ''' <param name="IDNum"></param>
    ''' <param name="Pos"></param>
    ''' <param name="DraftRound"></param>
    ''' <param name="RatingsStartPoint"></param>
    ''' <param name="RatingsAtTimeT"></param>
    ''' <param name="TimeT"></param>
    Public Sub GetKeyRatings(ByVal DT As DataTable, ByVal IDNum As Integer, ByVal Pos As String, DraftRound As String, Optional ByVal RatingsStartPoint As Integer = 90, Optional ByVal RatingsAtTimeT As Integer = 50, Optional ByVal TimeT As Integer = 7)
        Dim TimeValue As Integer
        Dim RatingsDecay As Double

        Dim KeyRatingsList As New List(Of String)
        KeyRatingsList = KeyRatings(Pos)

        Select Case DraftRound
            Case "R1Top5"
                TimeValue = 0
            Case "R1Top10"
                TimeValue = 1
            Case "R1MidFirst"
                TimeValue = 2
            Case "R1LateFirst"
                TimeValue = 3
            Case "R2"
                TimeValue = 4
            Case "R3"
                TimeValue = 5
            Case "R4"
                TimeValue = 6
            Case "R5"
                TimeValue = 7
            Case "R6"
                TimeValue = 8
            Case "R7"
                TimeValue = 9
            Case "PUFA"
                TimeValue = 10
            Case "LUFA"
                TimeValue = 11
            Case "PracSquad"
                TimeValue = 12
            Case "Reject"
                TimeValue = 13
        End Select
        Try
            RatingsDecay = GetRatingsDecay(RatingsStartPoint, RatingsAtTimeT, TimeT)

            For i As Integer = 0 To KeyRatingsList.Count - 1 'runs through the attribute key attribute list and aplpies the proper ratings based on round the player is generated for
                DT.Rows(IDNum).Item(String.Format("{0}", KeyRatingsList(i))) = RatingsStartPoint * ((1 + RatingsDecay) ^ TimeValue) + MT.GenerateInt32(-10, 10)
                'Console.WriteLine("{0}: {1}", KeyRatingsList(i), DT.Rows(IDNum).Item(String.Format("{0}", KeyRatingsList(i))))
            Next i
        Catch ex As system.ArgumentOutOfRangeException
            Console.WriteLine(ex.Data)
            Console.WriteLine(ex.Message)
        End Try
        KeyRatingsList.Clear()
    End Sub

    ''' <summary>
    ''' creates the decay between first round players and reject players---y(t)=ae^kt where y(t) is the value at the current time, a=initial time, k=rate of decay and t=time
    ''' </summary>
    ''' <returns></returns>
    Private Function GetRatingsDecay(ByVal InitialValue As Integer, ByVal ValueatTimeT As Integer, ByVal TimeT As Integer) As Single
        Dim k As Single
        k = (Math.Log(ValueatTimeT / InitialValue)) / TimeT
        Return k
    End Function
    ''' <summary>
    ''' These are the KeyRatings for the position---for instance, the ratings that determine what round a player is drafted in---Average of 50 is for a player drafted in round 5, results will be higher or lower based on that
    ''' List of ratings is returned
    ''' </summary>
    ''' <param name="Pos"></param>
    ''' <returns></returns>
    Private Function KeyRatings(ByVal Pos As String) As List(Of String)

        Dim result As New List(Of String)

        Select Case Pos
            Case "QB"
                result.Add("QBArmStrength")
                result.Add("QBAccuracy")
                result.Add("QBTouch")
                result.Add("Footwork")
                result.Add("QBRelQuickness")
                result.Add("QBLocateRec")
                result.Add("Leadership")
                result.Add("QBDecMaking")
                result.Add("QBMechanics")
                result.Add("QBPocketPresence")

            Case "RB"
                result.Add("RBRunVision")
                result.Add("QAB")
                result.Add("Explosion")
                result.Add("LowerBodyStrength")
                result.Add("ContactBalance")
                result.Add("COD")
                result.Add("RBPatience")
                result.Add("RBSetsUpBlocks")
                result.Add("Flexibility")
                result.Add("Instincts")

            Case "FB"
                result.Add("RunBlocking")
                result.Add("LowerBodyStrength")
                result.Add("Toughness")
                result.Add("Hands")
                result.Add("ContactBalance")
                result.Add("Reaction")
                result.Add("Explosion")
                result.Add("DeliversBlow")
                result.Add("QAB")
                result.Add("COD")

            Case "WR", "TE"
                result.Add("Explosion")
                result.Add("QAB")
                result.Add("COD")
                result.Add("ReleaseOffLine")
                result.Add("WRDisguiseRoute")
                result.Add("AdjustToBall")
                result.Add("RunAfterCatch")
                result.Add("Toughness")
                result.Add("Flexibility")
                result.Add("Hands")

            Case "OT"
                result.Add("HandUse")
                result.Add("PassBlockVsPower")
                result.Add("PassBlockVsSpeed")
                result.Add("RunBlocking")
                result.Add("Footwork")
                result.Add("QAB")
                result.Add("Flexibility")
                result.Add("Explosion")
                result.Add("OLRecover")
                result.Add("Reaction")

            Case "C"
                result.Add("HandUse")
                result.Add("OLAnchorAbility")
                result.Add("PassBlockVsPower")
                result.Add("RunBlocking")
                result.Add("Footwork")
                result.Add("QAB")
                result.Add("OLPulling")
                result.Add("Explosion")
                result.Add("Leadership")
                result.Add("Toughness")

            Case "OG"
                result.Add("HandUse")
                result.Add("OLAnchorAbility")
                result.Add("PassBlockVsPower")
                result.Add("RunBlocking")
                result.Add("Footwork")
                result.Add("QAB")
                result.Add("OLPulling")
                result.Add("Explosion")
                result.Add("Toughness")
                result.Add("Flexibility")

            Case "DE"
                result.Add("DefeatBlock")
                result.Add("ContactBalance")
                result.Add("Reaction")
                result.Add("HandUse")
                result.Add("QAB")
                result.Add("Explosion")
                result.Add("Flexibility")
                result.Add("ReadKeys")
                result.Add("Instincts")
                result.Add("COD")

            Case "DT"
                result.Add("Toughness")
                result.Add("ShedBlock")
                result.Add("Reaction")
                result.Add("HandUse")
                result.Add("QAB")
                result.Add("Explosion")
                result.Add("Flexibility")
                result.Add("ReadKeys")
                result.Add("DLRunAtHim")
                result.Add("COD")

            Case "OLB"
                result.Add("QAB")
                result.Add("COD")
                result.Add("Explosion")
                result.Add("Flexibility")
                result.Add("ReadKeys")
                result.Add("Reaction")
                result.Add("ShedBlock")
                result.Add("Tackling")
                result.Add("ZoneCoverage")
                result.Add("AvoidBlockers")

            Case "ILB"
                result.Add("QAB")
                result.Add("COD")
                result.Add("LBFillGaps")
                result.Add("Leadership")
                result.Add("ReadKeys")
                result.Add("Reaction")
                result.Add("ShedBlock")
                result.Add("Tackling")
                result.Add("AvoidBlockers")
                result.Add("Toughness")

            Case "CB"
                result.Add("Explosion")
                result.Add("QAB")
                result.Add("COD")
                result.Add("Flexibility")
                result.Add("Footwork")
                result.Add("DBBaitQB")
                result.Add("DBTechnique")
                result.Add("DBCatchUpSpeed")
                result.Add("ManToManCoverage")
                result.Add("AdjustToBall")

            Case "FS", "SS"
                result.Add("Explosion")
                result.Add("QAB")
                result.Add("COD")
                result.Add("Flexibility")
                result.Add("Footwork")
                result.Add("ReadKeys")
                result.Add("Tackling")
                result.Add("DBTechnique")
                result.Add("DeliversBlow")
                result.Add("DBBaitQB")

            Case "K"
                result.Add("KAccuracy")
                result.Add("Consistency")
                result.Add("Confidence")
                result.Add("Clutch")
                result.Add("Footwork")
                result.Add("LowerBodyStrength")
                result.Add("Explosion")
                result.Add("Reaction")
                result.Add("HandlesElements")
                result.Add("Flexibility")

            Case "P"
                result.Add("Flexibility")
                result.Add("PHangTime")
                result.Add("Footwork")
                result.Add("LowerBodyStrength")
                result.Add("Reaction") 'for catching bad snaps
                result.Add("Hands")
                result.Add("Consistency")
                result.Add("HandlesElements")
                result.Add("JumpingAbility")
                result.Add("Explosion")

        End Select

        Return result
    End Function
    ''' <summary>
    ''' Each Positional Type has its stregnths and weaknesses---this creates a list of those strengths and weaknesses and then gives a major(40-60% boost) or minor(20-30% boost) increase for strengths and a major or minor decrease for weaknesses
    ''' Primary weaknesses will be harder to improve than normal attributes as it is an INHERENT weakness of the style this player plays, and by the same token primary strengths will be slower to degrade
    ''' Each Position type has 2 Primary Strengths/Weaknesses and 2 Secondary Strengths/Weaknesses, other than Balanced which has a range of +/-10% on all attributes
    ''' </summary>
    ''' <param name="Pos"></param>
    ''' <param name="PosType"></param>
    ''' <param name="IDNum"></param>
    ''' <param name="DT"></param>
    Public Sub GetPosRatings(Pos As String, ByVal PosType As String, ByVal IDNum As Integer, ByVal DT As DataTable)
        Dim PrimStrength As New List(Of String)
        Dim SecStrength As New List(Of String)
        Dim PrimWeakness As New List(Of String)
        Dim SecWeakness As New List(Of String)
        Dim Balanced As New List(Of String)

        Select Case Pos
            Case "QB"
                Select Case PosType
                    Case "StrongArm" 'strong arm and confidence but often tries to force balls into coverage, makes bad decisions and is slow
                        PrimStrength.Add("QBArmStrength")
                        SecStrength.Add("QBMechanics")
                        SecStrength.Add("Toughness")
                        PrimStrength.Add("Confidence")
                        SecWeakness.Add("QBEscape")
                        PrimWeakness.Add("PlaySpeed")
                        SecWeakness.Add("QBDecMaking")
                        PrimWeakness.Add("QBTouch")

                    Case "WestCoast" 'good accuracy, touch and escapability but lacks arm strength and tends to be less durable, also tends to get routes jumped more often due to throwing short so much
                        PrimStrength.Add("QBAccuracy")
                        PrimStrength.Add("QBTouch")
                        SecStrength.Add("QBEscape")
                        SecStrength.Add("Footwork")
                        PrimWeakness.Add("QBArmStrength")
                        PrimWeakness.Add("FieldAwareness")
                        SecWeakness.Add("Durability")
                        SecWeakness.Add("Toughness")

                    Case "Balanced" 'No Big Boosts but no real weaknesses either
                        Balanced.Add("QBArmStrength")
                        Balanced.Add("QBAccuracy")
                        Balanced.Add("QBDecMaking")
                        Balanced.Add("QBTouch")
                        Balanced.Add("QBEscape")
                        Balanced.Add("QBPocketPresence")
                        Balanced.Add("Footwork")
                        Balanced.Add("QBRelQuickness")

                    Case "FieldGeneral" 'Leadership, LocateReceivers, FieldAwareness, Low Escape--stands in the pocket too long and takes sacks sometimes, Low Ballhandling gets stripped a lot from getting hit, Coachability is low because he thinks he knows it all
                        SecStrength.Add("QBLocateRec")
                        PrimStrength.Add("Leadership")
                        SecStrength.Add("FieldAwareness")
                        PrimStrength.Add("Clutch")
                        PrimWeakness.Add("BallSecurity")
                        PrimWeakness.Add("Coachability")
                        SecWeakness.Add("QBRelQuickness")
                        SecWeakness.Add("QBEscape")

                    Case "PocketPasser" 'Sits in the pocket and will carve a defense up, but tends to lose confidence easily if he starts getting hit. Not tough, moves around the pocket well but has issues escaping from the pocket
                        PrimStrength.Add("QBAccuracy")
                        PrimStrength.Add("QBTouch")
                        SecStrength.Add("QBArmstrength")
                        SecStrength.Add("QBPocketPresence")
                        PrimWeakness.Add("QBEscape")
                        PrimWeakness.Add("Confidence")
                        SecWeakness.Add("Toughness")
                        SecWeakness.Add("BallSecurity")


                    Case "Mobile" 'Mobile QB---boosts to playspeed, escape, rollout, QAB and COD--typically suffer from poor footwork, lack of pocket presence
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("QBScrambling")
                        SecStrength.Add("QBEscape")
                        SecStrength.Add("Instincts")
                        PrimWeakness.Add("Footwork")
                        PrimWeakness.Add("QBPocketPresence")
                        SecWeakness.Add("Durability")
                        SecWeakness.Add("QBMechanics")
                End Select

            Case "RB"
                Select Case PosType
                    Case "Balanced"
                        Balanced.Add("ContactBalance")
                        Balanced.Add("BreaksTackles")
                        Balanced.Add("Hands")
                        Balanced.Add("QAB")
                        Balanced.Add("COD")
                        Balanced.Add("RBPatience")
                        Balanced.Add("RBRunVision")
                        Balanced.Add("RBSetsUpBlocks")

                    Case "PowerBack" 'Toughness, Explosion, DeliversBlow, BreaksTackle but weak speed, QAB, COD and Patience
                        PrimStrength.Add("ContactBalance")
                        PrimStrength.Add("BreaksTackles")
                        SecStrength.Add("Toughness")
                        SecStrength.Add("DeliversBlow")
                        PrimWeakness.Add("PlaySpeed")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("COD")
                        SecWeakness.Add("RBPatience")

                    Case "SpeedBack"
                        PrimStrength.Add("Flexibility")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("COD")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("BreaksTackles")
                        PrimWeakness.Add("DeliversBlow")
                        SecWeakness.Add("Toughness")
                        SecWeakness.Add("ContactBalance")

                    Case "ReceivingBack"
                        PrimStrength.Add("Hands")
                        PrimStrength.Add("RouteRunning")
                        SecStrength.Add("RunAfterCatch")
                        SecStrength.Add("FieldAwareness")
                        PrimWeakness.Add("RBRunVision")
                        PrimWeakness.Add("RBSetsUpBlocks")
                        SecWeakness.Add("BreaksTackles")
                        SecWeakness.Add("Toughness")

                    Case "NorthSouthBack"
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("RBRunVision")
                        SecStrength.Add("ContactBalance")
                        SecStrength.Add("BreaksTackles")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("QAB")
                        SecWeakness.Add("Hands")
                        SecWeakness.Add("Durability")
                End Select
            Case "FB"

                Select Case PosType
                    Case "BatteringRam" 'typical blocking FB
                        PrimStrength.Add("RunBlocking")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("ContactBalance")
                        SecStrength.Add("LowerBodyStrength")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("RBRunVision")
                        SecWeakness.Add("RBSetsUpBlocks")
                        SecWeakness.Add("QAB")

                    Case "Balanced"
                        Balanced.Add("Runblocking")
                        Balanced.Add("Hands")
                        Balanced.Add("Explosion")
                        Balanced.Add("LowerBodyStrength")
                        Balanced.Add("RouteRunning")
                        Balanced.Add("QAB")
                        Balanced.Add("COD")
                        Balanced.Add("AdjustToBall")

                    Case "Receiving"
                        PrimStrength.Add("Hands")
                        PrimStrength.Add("RouteRunning")
                        SecStrength.Add("AdjustToBall")
                        SecStrength.Add("BodyCatch")
                        PrimWeakness.Add("RunBlocking")
                        PrimWeakness.Add("Explosion")
                        SecWeakness.Add("ContactBalance")
                        SecWeakness.Add("LowerBodyStrength")

                End Select
            Case "WR"
                Select Case PosType
                    Case "Balanced"
                        Balanced.Add("PlaySpeed")
                        Balanced.Add("Explosion")
                        Balanced.Add("Hands")
                        Balanced.Add("ReleaseOffLine")
                        Balanced.Add("QAB")
                        Balanced.Add("AdjustToBall")
                        Balanced.Add("RouteRunning")
                        Balanced.Add("RunBlocking")

                    Case "Speed"
                        PrimStrength.Add("Reaction")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("WRRunDBOff")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("ReleaseOffLine")
                        PrimWeakness.Add("Hands")
                        SecWeakness.Add("Durability")
                        SecWeakness.Add("BodyCatch")

                    Case "Possession"
                        PrimStrength.Add("Hands")
                        PrimStrength.Add("RouteRunning")
                        SecStrength.Add("ReleaseOffLine")
                        SecStrength.Add("RunBlocking")
                        PrimWeakness.Add("Explosion")
                        PrimWeakness.Add("RunAfterCatch")
                        SecWeakness.Add("PlaySpeed")
                        SecWeakness.Add("COD")

                    Case "Polished"
                        PrimStrength.Add("RouteRunning")
                        PrimStrength.Add("Runblocking")
                        SecStrength.Add("ReleaseOFfLine")
                        SecStrength.Add("FieldAwareness")
                        PrimWeakness.Add("ContactBalance")
                        PrimWeakness.Add("COD")
                        SecWeakness.Add("Concentration")
                        SecWeakness.Add("JumpingAbility")

                    Case "RZThreat"
                        PrimStrength.Add("JumpingAbility")
                        PrimStrength.Add("Hands")
                        SecStrength.Add("FieldAwareness")
                        SecStrength.Add("Aggressive")
                        PrimWeakness.Add("PlaySpeed")
                        PrimWeakness.Add("PlayBookKnowledge")
                        SecWeakness.Add("Flexibility")
                        SecWeakness.Add("QAB")

                End Select

            Case "TE"
                        Select Case PosType
                            Case "Balanced"
                                Balanced.Add("Playspeed")
                                Balanced.Add("QAB")
                                Balanced.Add("Toughness")
                                Balanced.Add("Hands")
                                Balanced.Add("Runblocking")
                                Balanced.Add("ReleaseOffLine")
                                Balanced.Add("Explosion")
                                Balanced.Add("RouteRunning")

                            Case "BlockingTE"
                                PrimStrength.Add("RunBlocking")
                                PrimStrength.Add("LowerBodyStrength")
                                SecStrength.Add("PassBlockVsPower")
                                SecStrength.Add("Toughness")
                                PrimWeakness.Add("PlaySpeed")
                                PrimWeakness.Add("Hands")
                                SecWeakness.Add("QAB")
                                SecWeakness.Add("COD")

                            Case "VerticalThreat"
                        PrimStrength.Add("QAB")
                        PrimStrength.Add("Explosion")
                                SecStrength.Add("ReleaseOffLine")
                                SecStrength.Add("Hands")
                                PrimWeakness.Add("RunBlocking")
                                PrimWeakness.Add("PassBlockVsPower")
                                SecWeakness.Add("LowerBodyStrength")
                                SecWeakness.Add("Toughness")

                            Case "Hybrid"
                                PrimStrength.Add("QAB")
                                PrimStrength.Add("COD")
                                SecStrength.Add("RunBlocking")
                                SecStrength.Add("Hands")
                                PrimWeakness.Add("Explosion")
                                PrimWeakness.Add("AdjustToBall")
                                SecWeakness.Add("Flexibility")
                                SecWeakness.Add("RouteRunning")

                            Case "Receiving"
                                PrimStrength.Add("RouteRunning")
                                PrimStrength.Add("Hands")
                                SecStrength.Add("QAB")
                                SecStrength.Add("AdjustToBall")
                                PrimWeakness.Add("PassBlockVsPower")
                                PrimWeakness.Add("Toughness")
                                SecWeakness.Add("RunBlocking")
                                SecWeakness.Add("ContactBalance")
                        End Select

                    Case "OT"
                        Select Case PosType
                            Case "LTProtoType" 'Strong passblocking Taclke versus speed, not always as good versus power, but not weak at it either
                                PrimStrength.Add("PassBlockVsSpeed")
                                PrimStrength.Add("Footwork")
                                SecStrength.Add("Flexibility")
                                SecStrength.Add("QAB")
                                PrimWeakness.Add("ContactBalance")
                                PrimWeakness.Add("Toughness")
                                SecWeakness.Add("RunBlocking")
                                SecWeakness.Add("OLAnchorAbility")

                            Case "RTProtoType" 'Run blocking tackle
                                PrimStrength.Add("RunBlocking")
                                PrimStrength.Add("OLAnchorAbility")
                                SecStrength.Add("Explosion")
                                SecStrength.Add("LowerBodyStrength")
                                PrimWeakness.Add("PassBlockVsSpeed")
                                PrimWeakness.Add("QAB")
                                SecWeakness.Add("OLMoveInSpace")
                                SecWeakness.Add("OLPulling")

                            Case "Balanced" '
                                Balanced.Add("PassBlockVsSpeed")
                                Balanced.Add("PassBlockVsPower")
                                Balanced.Add("Runblocking")
                                Balanced.Add("Flexibility")
                                Balanced.Add("ContactBalance")
                                Balanced.Add("HandUse")
                                Balanced.Add("QAB")
                                Balanced.Add("Footwork")

                            Case "AthleticLacksTechnique"
                                PrimStrength.Add("QAB")
                                PrimStrength.Add("Flexibility")
                                SecStrength.Add("OLRecover")
                                SecStrength.Add("PassBlockVsSpeed")
                                PrimWeakness.Add("Footwork")
                                PrimWeakness.Add("HandUse")
                                SecWeakness.Add("PassBlockVsPower")
                                SecWeakness.Add("ContactBalance")

                            Case "TechniqueLacksAthleticism"
                                PrimStrength.Add("HandUse")
                                PrimStrength.Add("Footwork")
                                SecStrength.Add("RunBlocking")
                                SecStrength.Add("OLRecover")
                                PrimWeakness.Add("Flexibility")
                                PrimWeakness.Add("QAB")
                                SecWeakness.Add("PassBlockVsPower")
                                SecWeakness.Add("PassBlockVsSpeed")
                        End Select

                    Case "C", "OG"
                        If Pos = "C" Then
                            DT.Rows(IDNum).Item("OLSnapAbility") = MT.GetGaussian(75, 8.33) 'this is not really a weakness or stength just a special job they have to do
                        End If
                        Select Case PosType

                            Case "Balanced"
                                Balanced.Add("OLPulling")
                                Balanced.Add("QAB")
                                Balanced.Add("PassBlockVsPower")
                                Balanced.Add("Flexibility")
                                Balanced.Add("HandUse")
                                Balanced.Add("Footwork")
                                Balanced.Add("RunBlocking")
                                Balanced.Add("PassBlockVsPower")

                            Case "RoadGrader" 'big strong, nasty run blocker but suffers in pass protection and is not quick enough to pull---mostly for man to man blocking schemes, not good for zone blocking schemes
                                PrimStrength.Add("RunBlocking")
                                PrimStrength.Add("LowerBodyStrength")
                                SecStrength.Add("ContactBalance")
                                SecStrength.Add("HandUse")
                                PrimWeakness.Add("PassBlockVsSpeed")
                                PrimWeakness.Add("QAB")
                                SecWeakness.Add("PassBlockVsPower")
                                SecWeakness.Add("Flexibility")

                            Case "RunBlocking" 'better run blocker than pass blocker
                                PrimStrength.Add("RunBlocking")
                                PrimStrength.Add("Explosion")
                                SecStrength.Add("OLPulling")
                                SecStrength.Add("HandUse")
                                PrimWeakness.Add("OLRecover")
                                PrimWeakness.Add("Reaction")
                                SecWeakness.Add("PassBlockVsPower")
                                SecWeakness.Add("PassBlockVsSpeed")

                            Case "ZoneBlocker" 'lighter, quicker OL used for pulling and getting to the second level in zone schemes, but struggle in strength
                                PrimStrength.Add("QAB")
                                PrimStrength.Add("OLPulling")
                                SecStrength.Add("OLMoveInSpace")
                                SecStrength.Add("Flexibility")
                                PrimWeakness.Add("PassBlockVsPower")
                                PrimWeakness.Add("LowerBodyStrength")
                                SecWeakness.Add("OLAnchorAbility")
                                SecWeakness.Add("ContactBalance")

                            Case "PassBlocker"
                                PrimStrength.Add("PassBlockVsPower")
                                PrimStrength.Add("PassBlockVsSpeed")
                                SecStrength.Add("Footwork")
                                SecStrength.Add("Flexibility")
                                PrimWeakness.Add("RunBlocking")
                                PrimWeakness.Add("OLAnchorAbility")
                                SecWeakness.Add("LowerBodyStrength")
                                SecWeakness.Add("HandUse")

                        End Select

                    Case "DE"
                        Select Case PosType
                            Case "Balanced4-3"
                                Balanced.Add("ReadKeys")
                                Balanced.Add("ShedBlock")
                                Balanced.Add("Flexibility")
                                Balanced.Add("Reaction")
                                Balanced.Add("Explosion")
                                Balanced.Add("DLSLideAbility")
                                Balanced.Add("DLRunAtHim")
                                Balanced.Add("COD")

                            Case "PrototypeLDE4-3" 'pass rushing DE---weaker against run
                                PrimStrength.Add("Explosion")
                                PrimStrength.Add("HandUse")
                                SecStrength.Add("Flexibility")
                                SecStrength.Add("QAB")
                                PrimWeakness.Add("LowerBodyStrength")
                                PrimWeakness.Add("DLRunAtHim")
                                SecWeakness.Add("ReadKeys") 'Too busy trying to get up field
                                SecWeakness.Add("FieldAwareness")

                            Case "ProtoTypeRDE4-3" 'run defending DE---typically not as good of a pass rusher
                                PrimStrength.Add("DLRunAtHim")
                                PrimStrength.Add("DLAgainstPullAbility")
                                SecStrength.Add("ShedBlock")
                                SecStrength.Add("LowerBodyStrength")
                                PrimWeakness.Add("Explosion")
                                PrimWeakness.Add("Flexibility")
                                SecWeakness.Add("QAB")
                                SecWeakness.Add("COD")

                            Case "Versatile3-4"
                                PrimStrength.Add("ReadKeys")
                                PrimStrength.Add("DLSlideAbility")
                                SecStrength.Add("COD")
                                SecStrength.Add("ShedBlock")
                                PrimWeakness.Add("Explosion")
                                PrimWeakness.Add("ContactBalance")
                                SecWeakness.Add("HandUse")
                                SecWeakness.Add("LowerBodyStrength")

                            Case "RunStopper3-4"
                                PrimStrength.Add("DLRunAtHim")
                                PrimStrength.Add("LowerBodyStrength")
                                SecStrength.Add("DLAgainstPullAbility")
                                SecStrength.Add("DLRunPursuit")
                                PrimWeakness.Add("Explosion")
                                PrimWeakness.Add("QAB")
                                SecWeakness.Add("COD")
                                SecWeakness.Add("DLSetUpPassRush")

                            Case "SituationalPassRusher" 'Guys that come in to rush on 3rd downs typically
                                PrimStrength.Add("Explosion")
                        PrimStrength.Add("QAB")
                        SecStrength.Add("DLSetUpPassRush")
                                SecStrength.Add("Reaction")
                                PrimWeakness.Add("PlayBookKnowledge")
                                PrimWeakness.Add("DLRunAtHim")
                                SecWeakness.Add("ReadKeys")
                                SecWeakness.Add("ContactBalance") 'only in there for obvious passing downs

                            Case "Hybrid" 'Tweener Type players
                        PrimStrength.Add("ZoneCoverage")
                        PrimStrength.Add("QAB")
                                SecStrength.Add("COD")
                                SecStrength.Add("Flexibility")
                                PrimWeakness.Add("DLRunAtHim")
                                PrimWeakness.Add("DLAgainstPullAbility")
                                SecWeakness.Add("HandUse")
                                SecWeakness.Add("LowerBodyStrength")
                        End Select

                    Case "DT"
                        Select Case PosType
                            Case "Penetrator" 'Athletic, quick 
                                PrimStrength.Add("Explosion")
                                PrimStrength.Add("HandUse")
                                SecStrength.Add("QAB")
                                SecStrength.Add("Reaction")
                                PrimWeakness.Add("ReadKeys")
                                PrimWeakness.Add("DLCanTakeDoubleTeam")
                                SecWeakness.Add("DLSlideAbility")
                                SecWeakness.Add("DLAgainstPullAbility")

                            Case "NoseTackle" 'Big, heavy, strong player used for ability to eat up blocks rather than make plays or penetrate
                                PrimStrength.Add("DLCanTakeDoubleTeam")
                                PrimStrength.Add("DLRunAtHim")
                                SecStrength.Add("LowerBodyStrength")
                                SecStrength.Add("ShedBlock")
                                PrimWeakness.Add("Flexibility")
                                PrimWeakness.Add("Explosion")
                                SecWeakness.Add("QAB")
                                SecWeakness.Add("COD")

                            Case "RunStopper"
                                PrimStrength.Add("DLRunAtHim")
                                PrimStrength.Add("DLSlideAbility")
                                SecStrength.Add("ShedBlock")
                                SecStrength.Add("Tackling")
                                PrimWeakness.Add("Flexibility")
                                PrimWeakness.Add("QAB")
                                SecWeakness.Add("Explosion")
                                SecWeakness.Add("COD")

                            Case "Balanced"
                                Balanced.Add("DLRunAtHim")
                                Balanced.Add("QAB")
                                Balanced.Add("ShedBlock")
                                Balanced.Add("DLSlideAbility")
                                Balanced.Add("Flexibility")
                                Balanced.Add("COD")
                                Balanced.Add("Explosion")
                                Balanced.Add("Reaction")

                            Case "Versatile"
                                PrimStrength.Add("QAB")
                                PrimStrength.Add("DLSlideAbility")
                                SecStrength.Add("ReadKeys")
                                SecStrength.Add("Flexibility")
                                PrimWeakness.Add("DLCanTakeDoubleTeam")
                                PrimWeakness.Add("DLRunAtHim")
                                SecWeakness.Add("HandUse")
                                SecWeakness.Add("LowerBodyStrength")
                        End Select
                    Case "OLB"
                        Select Case PosType
                            Case "WillProtoType4-3" 'must be able to play the run and rush the quarterback, but more than anything they are used in coverage.
                                PrimStrength.Add("QAB")
                                PrimStrength.Add("COD")
                                SecStrength.Add("ZoneCoverage")
                                SecStrength.Add("Flexibility")
                                PrimWeakness.Add("LBFillGaps")
                                PrimWeakness.Add("AvoidBlockers")
                                SecWeakness.Add("ShedBlock")
                                SecWeakness.Add("ContactBalance")

                            Case "PassRush3-4" 'quick, explosive passrusher
                                PrimStrength.Add("Explosion")
                                PrimStrength.Add("Flexibility")
                                SecStrength.Add("QAB")
                        SecStrength.Add("HandUse")
                        PrimWeakness.Add("AvoidBlockers")
                                PrimWeakness.Add("ReadKeys")
                                SecWeakness.Add("LBFillGaps")
                                SecWeakness.Add("LBDropDepth")

                            Case "Tweener4-3" 'Athletic but lacking in size, have tendency to get "engulfed" by bigger O-Line players
                        PrimStrength.Add("Explosion")
                        PrimStrength.Add("QAB")
                                SecStrength.Add("COD")
                                SecStrength.Add("Flexibility")
                                PrimWeakness.Add("LowerBodyStrength")
                                PrimWeakness.Add("AvoidBlockers")
                                SecWeakness.Add("Tackling")
                                SecWeakness.Add("ContactBalance")

                            Case "SamPrototype4-3" 'must have the power to take on blockers and attack the run, the flexibility to rush the quarterback off the edge & the feet to play in coverage against tight ends. Must be a complete player in a 4-3 defense.
                                PrimStrength.Add("LBFillGaps")
                                PrimStrength.Add("ShedBlock")
                                SecStrength.Add("AvoidBlockers")
                                SecStrength.Add("Explosion")
                                PrimWeakness.Add("COD")
                                PrimWeakness.Add("MantoManCoverage")
                                SecWeakness.Add("LBFillGaps")
                                SecWeakness.Add("AdjustToBall")

                            Case "Balanced"
                                Balanced.Add("LBFillGaps")
                                Balanced.Add("ShedBlock")
                                Balanced.Add("PlaySpeed")
                                Balanced.Add("Explosion")
                                Balanced.Add("Reaction")
                                Balanced.Add("Tackling")
                                Balanced.Add("QAB")
                                Balanced.Add("ReadKeys")
                        End Select

                    Case "ILB"
                        Select Case PosType
                            Case "MikeProtoType" 'Mike LB used in either system--Fiedl General/Do Everything backer
                                PrimStrength.Add("Leadership")
                                PrimStrength.Add("ReadKeys")
                                SecStrength.Add("Instincts")
                                SecStrength.Add("FieldAwareness")
                                PrimWeakness.Add("LBDropDepth")
                                PrimWeakness.Add("DefeatBlock")
                                SecWeakness.Add("COD")
                                SecWeakness.Add("AdjustToBall")

                            Case "TedProtoType3-4" 'Run Stopper LB
                                PrimStrength.Add("AvoidBlockers")
                                PrimStrength.Add("LBFillGaps")
                                SecStrength.Add("DeliversBlow")
                                SecStrength.Add("Tackling")
                                PrimWeakness.Add("ZoneCoverage")
                                PrimWeakness.Add("ManToManCoverage")
                                SecWeakness.Add("Flexibility")
                                SecWeakness.Add("PlaySpeed")

                            Case "Cover2ProtoType" 'Coverage, Deep Drops, speed, quickness
                                PrimStrength.Add("ZoneCoverage")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("LBDropDepth")
                                SecStrength.Add("QAB")
                                PrimWeakness.Add("ManToManCoverage")
                                PrimWeakness.Add("Tackling")
                                SecWeakness.Add("AvoidBlockers")
                                SecWeakness.Add("ShedBlock")

                            Case "TacklingMachine"  'Great Tackler
                                PrimStrength.Add("Tackling")
                                PrimStrength.Add("AvoidBlockers")
                                SecStrength.Add("ShedBlock")
                                SecStrength.Add("ReadKeys")
                                PrimWeakness.Add("ManToManCoverage")
                                PrimWeakness.Add("Flexibility")
                                SecWeakness.Add("LBDropDepth")
                                SecWeakness.Add("COD")

                            Case "Balanced"
                                Balanced.Add("Reaction")
                                Balanced.Add("ReadKeys")
                                Balanced.Add("Tackling")
                                Balanced.Add("ManToManCoverage")
                                Balanced.Add("QAB")
                                Balanced.Add("ShedBlock")
                                Balanced.Add("COD")
                                Balanced.Add("AvoidBlockers")
                        End Select
            Case "CB"
                Select Case PosType
                    Case "CoverCorner" 'great cover skills, not great in run support
                        PrimStrength.Add("ManToManCoverage")
                        PrimStrength.Add("Explosion")
                        SecStrength.Add("Flexibility")
                        SecStrength.Add("QAB")
                        PrimWeakness.Add("ZoneCoverage")
                        PrimWeakness.Add("DBRunContain")
                                SecWeakness.Add("AvoidBlockers")
                                SecWeakness.Add("ShedBlock")

                            Case "ZoneCorner" 'good zone cover skills but not man to man
                                PrimStrength.Add("ZoneCoverage")
                                PrimStrength.Add("QAB")
                                SecStrength.Add("COD")
                                SecStrength.Add("ReadKeys")
                                PrimWeakness.Add("ManToManCoverage")
                                PrimWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("Explosion")
                        SecWeakness.Add("Tackling")

                            Case "Balanced"
                                Balanced.Add("ManToManCoverage")
                                Balanced.Add("ZoneCoverage")
                                Balanced.Add("DBCatchUpSpeed")
                                Balanced.Add("PlaySpeed")
                                Balanced.Add("DBRunContain")
                                Balanced.Add("QAB")
                                Balanced.Add("COD")
                                Balanced.Add("Flexibility")

                            Case "RunSupport" 'Great CB for run support, lacks top end speed and agility to play great coverage tho
                                PrimStrength.Add("DBRunContain")
                                PrimStrength.Add("AvoidBlockers")
                                SecStrength.Add("Tackling")
                                SecStrength.Add("Reaction")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("QAB")
                                SecWeakness.Add("DBCatchUpSpeed")
                                SecWeakness.Add("Explosion")

                            Case "SlotCorner" 'very quick and agile, good blitzer off the slot and tackler but lacks top speed and size
                                PrimStrength.Add("QAB")
                                PrimStrength.Add("COD")
                                SecStrength.Add("Blitz")
                                SecStrength.Add("Tackling")
                                PrimWeakness.Add("DBRunContain")
                                PrimWeakness.Add("AvoidBlockers")
                                SecWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("ShedBlock")

                    Case "Physical" 'tough physical corner but has issues with faster receivers due to lacking top end speed, but helps out against the run as well
                                PrimStrength.Add("DBBump")
                                PrimStrength.Add("UpperBodyStrength")
                                SecStrength.Add("ManToManCoverage")
                                SecStrength.Add("DBRunContain")
                        PrimWeakness.Add("COD")
                        PrimWeakness.Add("QAB")
                                SecWeakness.Add("Explosion")
                                SecWeakness.Add("DBCatchUpSpeed")
                        End Select

            Case "FS", "SS"
                Select Case PosType
                    Case "Zone" 'good at zone coverage, but not so great at man
                        PrimStrength.Add("ZoneCoverage")
                        PrimStrength.Add("ReadKeys")
                        SecStrength.Add("COD")
                        SecStrength.Add("Reaction")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("AvoidBlockers")
                        SecWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("Explosion")

                    Case "Playmaker" 'All over the field making plays but a coverage liability and not the best tackler
                        PrimStrength.Add("Reaction")
                        PrimStrength.Add("DBBaitQB")
                        SecStrength.Add("ReadKeys")
                        SecStrength.Add("FieldAwareness")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("Tackling")
                        SecWeakness.Add("ShedBlock")
                        SecWeakness.Add("ZoneCoverage")

                    Case "Balanced"
                        Balanced.Add("ManToManCoverage")
                        Balanced.Add("ZoneCoverage")
                        Balanced.Add("Tackling")
                        Balanced.Add("Blitz")
                        Balanced.Add("ReadKeys")
                        Balanced.Add("AvoidBlockers")
                        Balanced.Add("QAB")
                        Balanced.Add("COD")

                    Case "RunSupport" 'big hitter and run support safety...coverage liability and slower
                        PrimStrength.Add("DBRunContain")
                        PrimStrength.Add("DeliversBlow")
                        SecStrength.Add("Tackling")
                        SecStrength.Add("AvoidBlockers")
                        PrimWeakness.Add("ManToManCoverage")
                        PrimWeakness.Add("ZoneCoverage")
                        SecWeakness.Add("QAB")
                        SecWeakness.Add("Explosion")

                    Case "Hybrid" 'a tweener between the two types
                        PrimStrength.Add("Flexibility")
                        PrimStrength.Add("QAB")
                        SecStrength.Add("MantoManCoverage")
                        SecStrength.Add("ZoneCoverage")
                        PrimWeakness.Add("AvoidBlockers")
                        PrimWeakness.Add("DBCatchUpSpeed")
                        SecWeakness.Add("DBRunContain")
                        SecWeakness.Add("Tackling")
                End Select

            Case "K"
                        Select Case PosType
                            Case "Clutch" 'makes the big kicks when it counts
                                PrimStrength.Add("Clutch")
                                SecStrength.Add("Confidence")
                                PrimWeakness.Add("Footwork") 'slower kick time
                                SecWeakness.Add("KAccuracy")

                            Case "Accurate" 'usually accurate but if he misses it can affect confidence for the remainder of the game or a few weeks
                                PrimStrength.Add("KAccuracy")
                                SecStrength.Add("Consistency")
                                PrimWeakness.Add("Footwork")
                                SecWeakness.Add("Confidence")

                            Case "Balanced"
                                Balanced.Add("Footwork")
                                Balanced.Add("KAccuracy")
                                Balanced.Add("HandlesElements")
                                Balanced.Add("Consistency")

                            Case "BigLeg"
                                PrimStrength.Add("LowerBodyStrength")
                                SecStrength.Add("Explosion")
                                PrimWeakness.Add("Footwork") 'slower kick times
                                SecWeakness.Add("KKickRise") 'lower trajectory

                            Case "KickOffSpecialist"
                                PrimStrength.Add("LowerBodyStrength")
                                SecStrength.Add("Explosion")
                                PrimWeakness.Add("KickAccuracy")
                                SecWeakness.Add("Consistency")
                        End Select

                    Case "P"
                        Select Case PosType
                            Case "BigLeg"
                                PrimStrength.Add("LowerBodyStrength")
                                SecStrength.Add("Explosion")
                                PrimWeakness.Add("Footwork") 'slower kick
                                SecWeakness.Add("AvoidBlockers") 'gets blocked more

                            Case "Accurate"
                                PrimStrength.Add("KickAccuracy")
                                SecStrength.Add("Consistency")
                                PrimWeakness.Add("LowerBodyStrength")
                                SecWeakness.Add("PHangTime")

                            Case "Balanced"
                                Balanced.Add("PHangTime")
                                Balanced.Add("LowerBodyStrength")
                                Balanced.Add("KickAccuracy")
                                Balanced.Add("Footwork")

                            Case "GreatHangTime"
                                PrimStrength.Add("PHangTime")
                                SecStrength.Add("Footwork")
                                PrimWeakness.Add("Consistency")
                                SecWeakness.Add("KickAccuracy")

                            Case "DirectionalPunter"
                                PrimStrength.Add("KickAccuracy")
                                SecStrength.Add("Footwork")
                                PrimWeakness.Add("Explosion")
                                SecWeakness.Add("LowerBodyStrength")

                            Case "AussieRules"
                                PrimStrength.Add("Reaction") 'catch bad punts
                                SecStrength.Add("Consistency")
                                PrimWeakness.Add("Footwork")
                                SecWeakness.Add("LowerBodyStrength")
                        End Select
                End Select

                For i As Integer = 0 To PrimStrength.Count - 1

            DT.Rows(IDNum).Item(PrimStrength(i)) *= MT.GenerateDouble(1.3, 1.5)
            'Console.WriteLine("{0}: {1}", PrimStrength(i), DT.Rows(IDNum).Item(PrimStrength(i)))
            DT.Rows(IDNum).Item(SecStrength(i)) *= MT.GenerateDouble(1.15, 1.25)
            'Console.WriteLine("{0}: {1}", SecStrength(i), DT.Rows(IDNum).Item(SecStrength(i)))
            DT.Rows(IDNum).Item(PrimWeakness(i)) *= MT.GenerateDouble(0.5, 0.7)
            'Console.WriteLine("{0}: {1}", PrimWeakness(i), DT.Rows(IDNum).Item(PrimWeakness(i)))
            DT.Rows(IDNum).Item(SecWeakness(i)) *= MT.GenerateDouble(0.75, 0.85)
            'Console.WriteLine("{0}: {1}", SecWeakness(i), DT.Rows(IDNum).Item(SecWeakness(i)))
        Next i



        If DT.Rows(IDNum).Item("PlaySpeed") > 100 Then
            DT.Rows(IDNum).Item("FortyYardTime") = 4.3
        Else
            DT.Rows(IDNum).Item("FortyYardTime") = Math.Round(((100 - DT.Rows(IDNum).Item("PlaySpeed")) / 83.3333) + 4.3, 2)
        End If

        If Balanced.Count <> 0 Then
            For i As Integer = 0 To Balanced.Count - 1
                DT.Rows(IDNum).Item(Balanced(i)) *= MT.GenerateDouble(0.9, 1.1)
            Next i
        End If
    End Sub

    Public Function GetPosType(ByVal Pos As String, IDNum As Integer) As String
        Dim result As String = ""
        Dim GetNum As Integer = MT.GenerateInt32(1, 100)
        Select Case Pos
            Case "QB" 'Gets the type of QB this player will be---stats will be generated within the framework set up for each subtype
                If DraftDT.Rows(IDNum).Item("FortyYardTime") < 4.65 Then
                    result = "Mobile"
                Else
                    Select Case GetNum
                        Case 1 To 10
                            result = "StrongArm"
                        Case 11 To 30
                            result = "WestCoast"
                        Case 31 To 40
                            result = "FieldGeneral"
                        Case 41 To 84
                            result = "Balanced"
                        Case Else
                            result = "PocketPasser"
                    End Select
                End If

            Case "RB"
                If DraftDT.Rows(IDNum).Item("FortyYardTime") < 4.47 Then
                    result = "SpeedBack"
                Else
                    Select Case GetNum
                        Case 1 To 40
                            result = "Balanced"
                        Case 41 To 58
                            result = "PowerBack"
                        Case 59 To 79
                            result = "Receiving"
                        Case Else
                            result = "OneCut"
                    End Select
                End If
            Case "FB"
                Select Case GetNum
                    Case 1 To 40
                        result = "BatteringRam"
                    Case 41 To 85
                        result = "Balanced"
                    Case Else
                        result = "Receiving"
                End Select
            Case "WR"
                If DraftDT.Rows(IDNum).Item("FortyYardTime") < 4.47 Then
                    result = "Speed"
                Else
                    Select Case GetNum
                        Case 1 To 44
                            result = "Balanced"
                        Case 46 To 65
                            result = "Possession"
                        Case 66 To 85
                            result = "Polished"
                        Case Else
                            result = "RZThreat"
                    End Select
                End If

            Case "TE"
                If DraftDT.Rows(IDNum).Item("FortyYardTime") < 4.61 Then
                    result = "VerticalThreat"
                Else
                    Select Case GetNum
                        Case 1 To 40
                            result = "Balanced"
                        Case 41 To 60
                            result = "Blocking"
                        Case 61 To 80
                            result = "Hybrid" 'HBack Type TE
                        Case Else
                            result = "Receiving"
                    End Select
                End If

            Case "OT"
                    Select Case GetNum
                        Case 1 To 10
                            result = "LTProtoType"
                        Case 11 To 30
                            result = "RTProtoType"
                        Case 31 To 70
                            result = "Balanced"
                        Case 71 To 85
                            result = "AthleticLacksTechnique"
                        Case 86 To 100
                            result = "TechniqueLacksAthleticism"
                    End Select

                    Case "C", "OG"
                    Select Case GetNum
                        Case 1 To 20
                            result = "RunBlocker"
                        Case 21 To 37
                            result = "RoadGrader"
                        Case 38 To 57
                            result = "ZoneBlocker"
                        Case 58 To 80
                            result = "Balanced"
                        Case Else
                            result = "PassBlocker"
                    End Select

                    Case "DE"
                    Select Case GetNum
                        Case 1 To 25
                            result = "Balanced4-3"
                        Case 26 To 35
                            result = "ProtoTypeLDE4-3"
                        Case 36 To 50
                            result = "ProtoTypeRDE4-3"
                        Case 51 To 65
                            result = "Versatile3-4"
                        Case 66 To 80
                            result = "RunStopper3-4"
                        Case 81 To 90
                            result = "SituationalPassRusher"
                        Case Else
                            result = "Hybrid"
                    End Select

                    Case "DT"
                    Select Case GetNum
                        Case 1 To 15
                            result = "Penetrator"
                        Case 16 To 30
                            result = "NoseTackle"
                        Case 31 To 50
                            result = "RunStopper"
                        Case 51 To 85
                            result = "Balanced"
                        Case Else
                            result = "Versatile"
                    End Select

                    Case "OLB"
                    Select Case GetNum
                        Case 1 To 19
                            result = "WillProtoType4-3" 'used more in coverage in a 3-4
                        Case 20 To 40
                            result = "Balanced"
                        Case 41 To 58
                            result = "PassRush3-4"
                        Case 59 To 70
                            result = "Tweener4-3" 'Athletic
                        Case Else
                            result = "SamPrototype4-3" 'must be a good run defender but also covers tight end---good all around player


                    End Select
                    Case "ILB"
                    Select Case GetNum
                        Case 1 To 20
                            result = "MikeProtoType" 'Mike LB used in either system--Fiedl General/Do Everything backer
                        Case 21 To 40
                            result = "TedProtoType3-4" 'Run Stopper LB
                        Case 41 To 60
                            result = "Cover2ProtoType" 'Coverage, Deep Drops, speed, quickness
                        Case 61 To 80
                            result = "TacklingMachine" 'Great Tackler
                        Case Else
                            result = "Balanced" 'comes in during Nickel situations for coverage skills

                    End Select

            Case "CB"
                If DraftDT.Rows(IDNum).Item("FortyYardTime") < 4.45 Then
                    result = "CoverCorner"
                Else
                    Select Case GetNum
                        Case 1 To 20
                            result = "ZoneCorner"
                        Case 21 To 64
                            result = "Balanced"
                        Case 65 To 75
                            result = "RunSupport"
                        Case 76 To 92
                            result = "SlotCorner"
                        Case Else
                            result = "Physical"
                    End Select
                End If

            Case "SS", "FS"
                Select Case GetNum
                    Case 1 To 20
                        result = "Zone"
                    Case 21 To 35
                        result = "PlayMaker"
                    Case 36 To 65
                        result = "Balanced"
                    Case 66 To 85
                        result = "RunSupport"
                    Case Else
                        result = "Hybrid"
                End Select

            Case "K"
                Select Case GetNum
                    Case 1 To 10
                        result = "Clutch"
                    Case 11 To 30
                        result = "Accurate"
                    Case 31 To 75
                        result = "Balanced"
                    Case 76 To 82
                        result = "KickoffSpecialist"
                    Case Else
                        result = "BigLeg"
                End Select

            Case "P"
                    Select Case GetNum
                        Case 1 To 15
                            result = "BigLeg"
                        Case 16 To 30
                            result = "DirectionalPunter"
                        Case 31 To 45
                            result = "Accurate"
                        Case 46 To 55
                            result = "AussieRules"
                        Case 56 To 70
                            result = "GreatHangTime"
                        Case Else
                            result = "Balanced"
                    End Select
        End Select
        Return result
    End Function
    Public Function GetKickRetAbility(ByVal Pos As String, ByVal i As Integer) As Integer
        'gets the ability to return kicks
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 75
                Return 0
            Case Else
                Select Case Pos
                    Case "RB", "WR", "CB", "FS"
                        Return MT.GetGaussian(49.5, 16.5)
                    Case Else
                        Return 0
                End Select
        End Select
    End Function
    Public Function GetPuntRetAbility(ByVal Pos As String, ByVal i As Integer) As Integer
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 75
                Return 0
            Case Else
                Select Case Pos
                    Case "RB", "WR", "CB", "FS"
                        Return PlayerDT.Rows(i).Item("RETKickReturn") = MT.GetGaussian(49.5, 16.5)
                    Case Else
                        Return 0
                End Select
        End Select
    End Function
    Public Sub GetSTAbility(ByVal Pos As String, ByVal i As Integer)

        Select Case Pos
                Case Not "QB", "K", "P", "DT"
                PlayerDT.Rows(i).Item("STCoverage") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("STWillingness") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("STAssignment") = MT.GetGaussian(49.5, 16.5)
                PlayerDT.Rows(i).Item("STDiscipline") = MT.GetGaussian(49.5, 16.5)
        End Select
    End Sub
    Public Sub GetLSAbility(ByVal Pos As String, ByVal i As Integer)
        Select Case Pos
            Case "C", "TE", "DE", "OG"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 90
                        PlayerDT.Rows(i).Item("OLLongSnapAbility") = 0
                    Case Else
                        PlayerDT.Rows(i).Item("OLLongSnapAbility") = MT.GetGaussian(49.5, 16.5)
                End Select
            Case Else
                PlayerDT.Rows(i).Item("OLLongSnapAbility") = 0
        End Select
    End Sub
End Class
