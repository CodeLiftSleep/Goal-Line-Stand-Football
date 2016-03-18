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
        Dim SQLFieldNames As String
        Select Case PlayerType
            Case "College"

                SQLFieldNames = "DraftID int PRIMARY KEY NOT NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL, DOB varchar(12) NULL, CollegePOS varchar(5) NULL, ActualGrade decimal(5,2) NULL, 
ProjNFLPos varchar(5) NULL, Height int NULL, Weight int NULL, ArmLength decimal(4,2) NULL, HandLength decimal(4,2) NULL, FortyYardTime decimal(3,2) NULL, TwentyYardTime decimal(3,2) NULL, TenYardTime decimal(3,2) NULL, ShortShuttle decimal(3,2) NULL, BroadJump int NULL, 
VertJump decimal(3,1) NULL, ThreeConeDrill decimal(3,2) NULL, BenchPress int NULL, InterviewSkills int NULL, WonderlicTest int NULL, SkillsTranslateToNFL int NULL, QBDropQuickness decimal(3,1) NULL, 
QBSetupQuickness decimal(3,1) NULL, QBReleaseQuickness decimal(3,1) NULL, QBShortAcc decimal(3,1) NULL, QBMedAcc decimal(3,1) NULL, QBLongAcc decimal(3,1) NULL, QBDecMaking decimal(3,1) NULL, QBFieldVision decimal(3,1) NULL, QBPoise decimal(3,1) NULL, 
QBBallHandling decimal(3,1) NULL, QBTiming decimal(3,1) NULL, QBDelivery decimal(3,1) NULL, QBFollowThrough decimal(3,1) NULL, QBAvoidRush decimal(3,1) NULL, QBEscape decimal(3,1) NULL, QBScrambling decimal(3,1) NULL, QBRolloutRight decimal(3,1) NULL, 
QBRolloutLeft decimal(3,1) NULL, QBArmStrength decimal(3,1) NULL, QBZip decimal(3,1) NULL, QBTouchScreenPass decimal(3,1) NULL, QBTouchSwingPass decimal(3,1) NULL, QBEffectiveShortOut decimal(3,1) NULL, QBEffectiveGoRoute decimal(3,1) NULL, 
QBEffectivePostRoute decimal(3,1) NULL, QBEffectiveCornerRoute decimal(3,1) NULL, QBEffectiveDeepOut decimal(3,1) NULL, RBEffortBlocking decimal(3,1) NULL, RBDurability decimal(3,1) NULL, RBPassBlocking decimal(3,1) NULL, 
RBHands decimal(3,1) NULL, RBStart decimal(3,1) NULL, RBRunVision decimal(3,1) NULL, RBInsideAbility decimal(3,1) NULL, RBOutsideAbility decimal(3,1) NULL, RBElusiveAbility decimal(3,1) NULL, RBPowerAbility decimal(3,1) NULL, 
RBRunBlocking decimal(3,1) NULL, RBRouteRunning decimal(3,1) NULL, RBRunningStyle varchar(15) NULL, Athleticism decimal(3,1) NULL, QAB decimal(3,1) NULL, COD decimal(3,1) NULL, WRShortRoute decimal(3,1) NULL, WRCrowdReaction decimal(3,1) NULL, 
WRConcentration decimal(3,1) NULL, WRFieldAwareness decimal(3,1) NULL, WRBodyCatch decimal(3,1) NULL, WRBlocking decimal(3,1) NULL, WRRelease decimal(3,1) NULL, WRStart decimal(3,1) NULL, WRPatterns decimal(3,1) NULL, WRMedRoute decimal(3,1) NULL, 
WRDeepRoute decimal(3,1) NULL, WRBallAdjust decimal(3,1) NULL, WRHandCatch decimal(3,1) NULL, WRRAC decimal(3,1) NULL, WRCatchWhenHit decimal(3,1) NULL, TEGetOffLineRunBlock decimal(3,1) NULL, TEOneOnOneBlocking decimal(3,1) NULL, 
TEDoubleTeamBlocking decimal(3,1) NULL, TEDownBlocking decimal(3,1) NULL, TETurnAndWallBlocking decimal(3,1) NULL, TEsustainBlocking decimal(3,1) NULL, TECrowdReaction decimal(3,1) NULL, TECatchWhenHit decimal(3,1) NULL, TEConcentration decimal(3,1) NULL,
TEBodyCatch decimal(3,1) NULL, TEFieldAwareness decimal(3,1) NULL, TEPassProtect decimal(3,1) NULL, TEDriveIntoPassRoute decimal(3,1) NULL, TEPatterns decimal(3,1) NULL, TEShortRoute decimal(3,1) NULL, TEMedRoute decimal(3,1) NULL, 
TEDeepRoute decimal(3,1) NULL, TEBallAdjust decimal(3,1) NULL, TEHandCatch decimal(3,1) NULL, TERAC decimal(3,1) NULL, OLGetOutside decimal(3,1) NULL, OLReachBlock decimal(3,1) NULL, OLTurnDefender decimal(3,1) NULL, OLPulling decimal(3,1) NULL, 
OLTrapBlock decimal(3,1) NULL, OL2ndLevelPull decimal(3,1) NULL, OLAdjusttoLB decimal(3,1) NULL, OLSlide decimal(3,1) NULL, OLHandUse decimal(3,1) NULL, OLHandPop decimal(3,1) NULL, OLLongSnapPotential decimal(3,1) NULL, 
OLGetOffLineRunBlock decimal(3,1) NULL, OLOneOnOneBlocking decimal(3,1) NULL, OLDriveBlocking decimal(3,1) NULL, OLDownBlocking decimal(3,1) NULL, OLSustainBlock decimal(3,1) NULL, OLPassBlocking decimal(3,1) NULL, OLPassDrops decimal(3,1) NULL, 
OLFeetSetup decimal(3,1) NULL, OLAnchorAbility decimal(3,1) NULL, OLRecover decimal(3,1) NULL, OLStrength decimal(3,1) NULL, DLStyle varchar(15) NULL, DLRunAtHim decimal(3,1) NULL, DLTackling decimal(3,1) NULL, DLAgainstTrapAbility decimal(3,1) NULL, 
DLSlideAbility decimal(3,1) NULL, DLRunPursuit decimal(3,1) NULL, DLPassRushTechnique varchar(15) NULL, DLHandUse decimal(3,1) NULL, DLShedVsRunAway decimal(3,1) NULL, DLShedRunBlock decimal(3,1) NULL, DLTackleVsRunAway decimal(3,1) NULL, DLContain int NULL, DLStayAtHome int NULL,
DLChangeDirection decimal(3,1) NULL, DLReleaseOffBall decimal(3,1) NULL, DLOneOnOneAbility decimal(3,1) NULL, DLDoubleTeamAbility decimal(3,1) NULL, DLDefeatBlock decimal(3,1) NULL, DLFirstStepPassRush decimal(3,1) NULL, DLShedPassBlock decimal(3,1) NULL,
DLBurst decimal(3,1) NULL, DLPressure decimal(3,1) NULL, DLFinish decimal(3,1) NULL, LBDropDepth decimal(3,1) NULL, LBCoverage decimal(3,1) NULL, LBHands decimal(3,1) NULL, LBBlitz decimal(3,1) NULL, LBPassRushType varchar(15) NULL, LBContain decimal(3,1) NULL, LBFillGaps decimal(3,1) NULL,
LBRead decimal(3,1) NULL, LBInstincts decimal(3,1) NULL, LBDefeatBlocks decimal(3,1) NULL, LBShedBlocks decimal(3,1) NULL, LBInsideTackle decimal(3,1) NULL, LBOutsideTackle decimal(3,1) NULL, DBPressBailCoverage decimal(3,1) NULL, 
DBBallReaction decimal(3,1) NULL, DBTackling decimal(3,1) NULL, DBZoneCoverage decimal(3,1) NULL, DBRuncontain decimal(3,1) NULL, DBWardOffBlockers decimal(3,1) NULL, DBRunTackling decimal(3,1) NULL, DBHands decimal(3,1) NULL,
DBBump decimal(3,1) NULL, DDBRunContain decimal(3,1) NULL, DBBlitz decimal(3,1) NULL, DBRead decimal(3,1) NULL, DBInstincts decimal(3,1) NULL, DBBackpedal decimal(3,1) NULL, DBTurn decimal(3,1) NULL, DBClose decimal(3,1) NULL, DBRange decimal(3,1) NULL, 
DBCatchUpSpeed decimal(3,1) NULL, DBManToManCoverage decimal(3,1) NULL, DBBurst decimal(3,1) NULL, DBCOBP decimal(3,1) NULL, DBFeet decimal(3,1) NULL, KPlantRelationship varchar(15) NULL, KApproachAngle varchar(15) NULL, KBallFlight varchar(15) NULL, 
KSteppingPattern varchar(15) NULL, KKickingStyle varchar(15) NULL, KHandlingWind decimal(3,1) NULL, KTackling decimal(3,1) NULL, KRunAndPassAbility decimal(3,1) NULL, KKOFootSpeed decimal(3,1) NULL, KFGOperationTimes decimal(3,1) NULL, 
KAccuracy decimal(3,1) NULL, KHandlingPressure decimal(3,1) NULL, KFootSpeed decimal(3,1) NULL, KKickQuickness decimal(3,1) NULL, KKickRise decimal(3,1) NULL, KKOProduction decimal(3,1) NULL, KKOMentalStability decimal(3,1) NULL, 
PFootSpeed decimal(3,1) NULL, PApproachLine decimal(3,1) NULL, PHandlingTime decimal(3,1) NULL, PSteppingPattern varchar(15) NULL, PHands decimal(3,1) NULL, PTackling decimal(3,1) NULL, PRunAndPassAbility decimal(3,1) NULL, PDistance decimal(3,1) NULL, 
PHangTime decimal(3,1) NULL, PPressureKicking decimal(3,1) NULL, PBlockZone decimal(3,1) NULL, PHandToFootTime decimal(3,1) NULL, PTiming decimal(3,1) NULL, Flexibility decimal(3,1) NULL, Clutch decimal(3,1) NULL, Production decimal(3,1) NULL, 
Consistency decimal(3,1) NULL, TeamPlayer decimal(3,1) NULL, Instincts decimal(3,1) NULL, Focus decimal(3,1) NULL, PlayStrength decimal(3,1) NULL, Explosion decimal(3,1) NULL, DeliversBlow decimal(3,1) NULL, RiskTaker decimal(3,1) NULL,
JumpingAbility decimal(3,1), Leadership decimal(3,1) NULL, Aggressive decimal(3,1) NULL,  Fearless decimal(3,1) NULL, FieldAwareness decimal(3,1) NULL, WorkEthic decimal(3,1) NULL, FilmStudy decimal(3,1),
Character decimal(3,1) NULL, Toughness decimal(3,1) NULL, InjuryProne decimal(3,1) NULL, PlaybookKnowledge decimal(3,1) NULL, BallSecurity decimal(3,1), Timing decimal(3,1) NULL"

                Return SQLFieldNames

            Case "NFL"

                SQLFieldNames = "PlayerID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL, DOB varchar(12) NULL, Height int NULL, Weight int NULL,
ArmLength decimal(4,2) NULL, HandLength decimal(4,2) NULL, POS varchar(4) NULL, QAB int NULL, COD int NULL, QBDropQuickness int NULL, QBSetupQuickness int NULL, QBReleaseQuickness int NULL, QBShortAcc int NULL, QBMedAcc int NULL, 
QBLongAcc int NULL, QBDecMaking int NULL, QBFieldVision int NULL, QBDeliveryQuickness int NULL, QBPoise int NULL, QBBallHandling int NULL, QBTiming int NULL, QBDelivery int NULL, QBFollowThrough int NULL, 
QBAvoidRush int NULL, QBEscape int NULL, QBScrambling int NULL, QBRolloutRight int NULL, QBRolloutLeft int NULL, QBArmStrength int NULL, QBZip int NULL, QBTouchScreenPass int NULL, QBTouchSwingPass int NULL, QBEffectiveShortOut int NULL, 
QBEffectiveGoRoute int NULL, QBEffectivePostRoute int NULL, QBEffectiveCornerRoute int NULL, QBEffectiveDeepOut int NULL, RBEffortBlocking int NULL, RBDurability int NULL, RBPassBlocking int NULL, RBHands int NULL, RBStart int NULL, RBRunVision int NULL, 
RBInsideAbility int NULL, RBBallAdjust int NULL, RBOutsideAbility int NULL, RBElusiveAbility int NULL, RBPowerAbility int NULL, RBRunBlocking int NULL, RBRouteRunning int NULL, RBRunningStyle varchar(15) NULL, Athleticism int NULL, WRShortRoute int NULL, 
WRCrowdReaction int NULL, WRConcentration int NULL, WRRunDBOff int NULL, WRAcrobaticCatches int NULL, WRFieldAwareness int NULL, WRBodyCatch int NULL, WRBlocking int NULL, WRRelease int NULL, WRStart int NULL, WRPatterns int NULL, WRMedRoute int NULL, 
WRDeepRoute int NULL, WRBallAdjust int NULL, WRHandCatch int NULL, WRRAC int NULL, WRCatchWhenHit int NULL, TEGetOffLineRunBlock int NULL, TEOneOnOneBlocking int NULL, TEDoubleTeamBlocking int NULL, TEDownBlocking int NULL, TETurnAndWallBlocking int NULL, 
TEsustainBlocking int NULL, TECrowdReaction int NULL, TECatchWhenHit int NULL, TEConcentration int NULL, TEBodyCatch int NULL, TEFieldAwareness int NULL, TEPassProtect int NULL, TEDriveIntoPassRoute int NULL, TEPatterns int NULL, TEShortRoute int NULL, 
TEMedRoute int NULL, TEDeepRoute int NULL, TEBallAdjust int NULL, TEHandCatch int NULL, TERAC int NULL, OLGetOutside int NULL, OLReachBlock int NULL, OLTurnDefender int NULL, OLPulling int NULL, OLTrapBlock int NULL, OL2ndLevelPull int NULL, OLAdjusttoLB int NULL, 
OLSlide int NULL, OLHandUse int NULL, OLHandPop int NULL, OLLongSnapPotential int NULL, OLGetOffLineRunBlock int NULL, OLOneOnOneBlocking int NULL, OLDriveBlocking int NULL, OLDownBlocking int NULL, OLSustainBlock int NULL, OLPassBlocking int NULL,
OLPassDrops int NULL, OLFeetSetup int NULL, OLInSpace int NULL, OLLongSnapPot int NULL, OLAnchorAbility int NULL, OLRecover int NULL, OLStrength int NULL, DLStyle varchar(15) NULL, DLRunAtHim int NULL, DLTackling int NULL, DLAgainstTrapAbility int NULL, 
DLSlideAbility int NULL, DLRunPursuit int NULL, DLPassRushTechnique varchar(15) NULL, DLHandUse int NULL, DLShedVsRunAway int NULL, DLShedRunBlock int NULL, DLTackleVsRunAway int NULL, DLChangeDirection int NULL, DLReleaseOffBall int NULL, DLContain int NULL, DLStayAtHome int NULL, 
DLOneOnOneAbility int NULL, DLDoubleTeamAbility int NULL, DLDefeatBlock int NULL, DLFirstStepPassRush int NULL, DLShedPassBlock int NULL, DLBurst int NULL, DLPressure int NULL, DLFinish int NULL, LBDropDepth int NULL, LBCoverage int NULL, LBHands int NULL, 
LBBlitz int NULL, LBPassRushType varchar(15) NULL, LBFillGaps int NULL, LBContain int NULL, LBRead int NULL, LBInstincts int NULL, LBDefeatBlocks int NULL, LBShedBlocks int NULL, LBInsideTackle int NULL, LBOutsideTackle int NULL, DBPressBailCoverage int NULL, 
DBBallReaction int NULL, DBTackling int NULL, DBZoneCoverage int NULL, DBRuncontain int NULL, DBWardOffBlockers int NULL, DBRunTackling int NULL, DBHands int NULL, DBTurnAndRun int NULL, DBBump int NULL, DDBRunContain int NULL, DBBlitz int NULL, DBRead int NULL, 
DBInstincts int NULL, DBBackpedal int NULL, DBTurn int NULL, DBClose int NULL, DBRange int NULL, DBCatchUpSpeed int NULL, DBManToManCoverage int NULL, DBBurst int NULL, DBCOBP int NULL, DBFeet int NULL, KPlantRelationship varchar(15) NULL, 
KApproachAngle varchar(15) NULL, KBallFlight varchar(15) NULL, KSteppingPattern varchar(15) NULL, KKickingStyle varchar(15) NULL, KHandlingWind int NULL, KTackling int NULL, KRunAndPassAbility int NULL, KKOFootSpeed int NULL, KFGOperationTimes int NULL, 
KAccuracy int NULL, KHandlingPressure int NULL, KFootSpeed int NULL, KKickQuickness int NULL, KKickRise int NULL, KKOProduction int NULL, KKOMentalStability int NULL, PFootSpeed int NULL, PApproachLine int NULL, PHandlingTime int NULL,
PSteppingPattern varchar(15) NULL, PHands int NULL, PTackling int NULL, PRunAndPassAbility int NULL, PDistance int NULL, PHangTime int NULL, PPressureKicking int NULL, PBlockZone int NULL, PHandToFootTime int NULL, PTiming int NULL, STCoverage int NULL, 
STWillingness int NULL, STAssignment int NULL, STDiscipline int NULL, Flexibility int NULL, Clutch int NULL, Production int NULL, Consistency int NULL, TeamPlayer int NULL, Instincts int NULL, Focus int NULL,  RETKickReturn int NULL,RETPuntReturn int NULL, 
PlayStrength int NULL, FilmStudy int NULL, Durability int NULL, Explosion int NULL, JumpingAbility int NULL, DeliversBlow int NULL, Leadership int NULL, Character int NULL, Toughness int NULL, InjuryProne int NULL, WorkEthic int NULL,
Aggressive int NULL,  Fearless int NULL, FieldAwareness int NULL, PlaybookKnowledge int NULL, RiskTaker int NULL, LSSnapAccuracy int NULL, LSSnapTime int NULL, BallSecurity int NULL, Timing int NULL"

                Return SQLFieldNames
        End Select
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

    Public Function GetKickRetAbility(ByVal Pos As String, ByVal i As Integer) As Integer
        'gets the ability to return kicks
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 75
                Return 0
            Case Else
                Select Case Pos
                    Case "RB", "WR", "CB"
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
        Select Case MT.GenerateInt32(1, 100)
            Case 26 To 75
                Select Case Pos
                    Case "QB", "K", "P", "DT"
                        PlayerDT.Rows(i).Item("STCoverage") = 0
                        PlayerDT.Rows(i).Item("STWillingness") = 0
                        PlayerDT.Rows(i).Item("STAssignment") = 0
                        PlayerDT.Rows(i).Item("STDiscipline") = 0
                    Case Else
                        PlayerDT.Rows(i).Item("STCoverage") = MT.GetGaussian(49.5, 16.5)
                        PlayerDT.Rows(i).Item("STWillingness") = MT.GetGaussian(49.5, 16.5)
                        PlayerDT.Rows(i).Item("STAssignment") = MT.GetGaussian(49.5, 16.5)
                        PlayerDT.Rows(i).Item("STDiscipline") = MT.GetGaussian(49.5, 16.5)
                End Select
        End Select
    End Sub
    Public Sub GetLSAbility(ByVal Pos As String, ByVal i As Integer)
        Select Case Pos
            Case "C", "TE", "DE", "OG"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 90
                        PlayerDT.Rows(i).Item("LSSnapTime") = 0
                        PlayerDT.Rows(i).Item("LSSnapAccuracy") = 0
                    Case Else
                        PlayerDT.Rows(i).Item("LSSnapTime") = MT.GetGaussian(49.5, 16.5)
                        PlayerDT.Rows(i).Item("LSSnapAccuracy") = MT.GetGaussian(49.5, 16.5)
                End Select
            Case Else
                PlayerDT.Rows(i).Item("LSSnapTime") = 0
                PlayerDT.Rows(i).Item("LSSnapAccuracy") = 0
        End Select
    End Sub
End Class
