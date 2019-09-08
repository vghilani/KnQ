#pragma warning disable 612,618
#pragma warning disable 0114
#pragma warning disable 0108

using System;
using System.Collections.Generic;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!

namespace GameSparks.Api.Requests{
		public class LogEventRequest_acceptFriendRequest : GSTypedRequest<LogEventRequest_acceptFriendRequest, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_acceptFriendRequest() : base("LogEventRequest"){
			request.AddString("eventKey", "acceptFriendRequest");
		}
		
		public LogEventRequest_acceptFriendRequest Set_request_id( string value )
		{
			request.AddString("request_id", value);
			return this;
		}
		
		public LogEventRequest_acceptFriendRequest Set_accepterid( string value )
		{
			request.AddString("accepterid", value);
			return this;
		}
		
		public LogEventRequest_acceptFriendRequest Set_accepterName( string value )
		{
			request.AddString("accepterName", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_acceptFriendRequest : GSTypedRequest<LogChallengeEventRequest_acceptFriendRequest, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_acceptFriendRequest() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "acceptFriendRequest");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_acceptFriendRequest SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_acceptFriendRequest Set_request_id( string value )
		{
			request.AddString("request_id", value);
			return this;
		}
		public LogChallengeEventRequest_acceptFriendRequest Set_accepterid( string value )
		{
			request.AddString("accepterid", value);
			return this;
		}
		public LogChallengeEventRequest_acceptFriendRequest Set_accepterName( string value )
		{
			request.AddString("accepterName", value);
			return this;
		}
	}
	
	public class LogEventRequest_advTurn : GSTypedRequest<LogEventRequest_advTurn, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_advTurn() : base("LogEventRequest"){
			request.AddString("eventKey", "advTurn");
		}
		
		public LogEventRequest_advTurn Set_pid( string value )
		{
			request.AddString("pid", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_advTurn : GSTypedRequest<LogChallengeEventRequest_advTurn, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_advTurn() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "advTurn");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_advTurn SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_advTurn Set_pid( string value )
		{
			request.AddString("pid", value);
			return this;
		}
	}
	
	public class LogEventRequest_DeclineGame : GSTypedRequest<LogEventRequest_DeclineGame, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_DeclineGame() : base("LogEventRequest"){
			request.AddString("eventKey", "DeclineGame");
		}
		
		public LogEventRequest_DeclineGame Set_chalid( string value )
		{
			request.AddString("chalid", value);
			return this;
		}
		
		public LogEventRequest_DeclineGame Set_myid( string value )
		{
			request.AddString("myid", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_DeclineGame : GSTypedRequest<LogChallengeEventRequest_DeclineGame, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_DeclineGame() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "DeclineGame");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_DeclineGame SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_DeclineGame Set_chalid( string value )
		{
			request.AddString("chalid", value);
			return this;
		}
		public LogChallengeEventRequest_DeclineGame Set_myid( string value )
		{
			request.AddString("myid", value);
			return this;
		}
	}
	
	public class LogEventRequest_DrawVote : GSTypedRequest<LogEventRequest_DrawVote, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_DrawVote() : base("LogEventRequest"){
			request.AddString("eventKey", "DrawVote");
		}
		
		public LogEventRequest_DrawVote Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		
		public LogEventRequest_DrawVote Set_WaitingVotes( string value )
		{
			request.AddString("WaitingVotes", value);
			return this;
		}
		
		public LogEventRequest_DrawVote Set_VoteCast( string value )
		{
			request.AddString("VoteCast", value);
			return this;
		}
		
		public LogEventRequest_DrawVote Set_NoVotes( string value )
		{
			request.AddString("NoVotes", value);
			return this;
		}
		
		public LogEventRequest_DrawVote Set_YesVotes( string value )
		{
			request.AddString("YesVotes", value);
			return this;
		}
		
		public LogEventRequest_DrawVote Set_GameName( string value )
		{
			request.AddString("GameName", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_DrawVote : GSTypedRequest<LogChallengeEventRequest_DrawVote, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_DrawVote() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "DrawVote");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_DrawVote SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_DrawVote Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogChallengeEventRequest_DrawVote Set_WaitingVotes( string value )
		{
			request.AddString("WaitingVotes", value);
			return this;
		}
		public LogChallengeEventRequest_DrawVote Set_VoteCast( string value )
		{
			request.AddString("VoteCast", value);
			return this;
		}
		public LogChallengeEventRequest_DrawVote Set_NoVotes( string value )
		{
			request.AddString("NoVotes", value);
			return this;
		}
		public LogChallengeEventRequest_DrawVote Set_YesVotes( string value )
		{
			request.AddString("YesVotes", value);
			return this;
		}
		public LogChallengeEventRequest_DrawVote Set_GameName( string value )
		{
			request.AddString("GameName", value);
			return this;
		}
	}
	
	public class LogEventRequest_findPlayers : GSTypedRequest<LogEventRequest_findPlayers, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_findPlayers() : base("LogEventRequest"){
			request.AddString("eventKey", "findPlayers");
		}
		public LogEventRequest_findPlayers Set_query( GSData value )
		{
			request.AddObject("query", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_findPlayers : GSTypedRequest<LogChallengeEventRequest_findPlayers, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_findPlayers() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "findPlayers");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_findPlayers SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_findPlayers Set_query( GSData value )
		{
			request.AddObject("query", value);
			return this;
		}
		
	}
	
	public class LogEventRequest_ForgotPasswordSearch : GSTypedRequest<LogEventRequest_ForgotPasswordSearch, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_ForgotPasswordSearch() : base("LogEventRequest"){
			request.AddString("eventKey", "ForgotPasswordSearch");
		}
		
		public LogEventRequest_ForgotPasswordSearch Set_email( string value )
		{
			request.AddString("email", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_ForgotPasswordSearch : GSTypedRequest<LogChallengeEventRequest_ForgotPasswordSearch, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_ForgotPasswordSearch() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "ForgotPasswordSearch");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_ForgotPasswordSearch SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_ForgotPasswordSearch Set_email( string value )
		{
			request.AddString("email", value);
			return this;
		}
	}
	
	public class LogEventRequest_ForgotPasswordSend : GSTypedRequest<LogEventRequest_ForgotPasswordSend, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_ForgotPasswordSend() : base("LogEventRequest"){
			request.AddString("eventKey", "ForgotPasswordSend");
		}
		
		public LogEventRequest_ForgotPasswordSend Set_email( string value )
		{
			request.AddString("email", value);
			return this;
		}
		
		public LogEventRequest_ForgotPasswordSend Set_userid( string value )
		{
			request.AddString("userid", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_ForgotPasswordSend : GSTypedRequest<LogChallengeEventRequest_ForgotPasswordSend, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_ForgotPasswordSend() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "ForgotPasswordSend");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_ForgotPasswordSend SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_ForgotPasswordSend Set_email( string value )
		{
			request.AddString("email", value);
			return this;
		}
		public LogChallengeEventRequest_ForgotPasswordSend Set_userid( string value )
		{
			request.AddString("userid", value);
			return this;
		}
	}
	
	public class LogEventRequest_friendRequest : GSTypedRequest<LogEventRequest_friendRequest, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_friendRequest() : base("LogEventRequest"){
			request.AddString("eventKey", "friendRequest");
		}
		
		public LogEventRequest_friendRequest Set_player_id( string value )
		{
			request.AddString("player_id", value);
			return this;
		}
		
		public LogEventRequest_friendRequest Set_message( string value )
		{
			request.AddString("message", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_friendRequest : GSTypedRequest<LogChallengeEventRequest_friendRequest, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_friendRequest() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "friendRequest");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_friendRequest SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_friendRequest Set_player_id( string value )
		{
			request.AddString("player_id", value);
			return this;
		}
		public LogChallengeEventRequest_friendRequest Set_message( string value )
		{
			request.AddString("message", value);
			return this;
		}
	}
	
	public class LogEventRequest_friendRequestDeclined : GSTypedRequest<LogEventRequest_friendRequestDeclined, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_friendRequestDeclined() : base("LogEventRequest"){
			request.AddString("eventKey", "friendRequestDeclined");
		}
		
		public LogEventRequest_friendRequestDeclined Set_request_id( string value )
		{
			request.AddString("request_id", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_friendRequestDeclined : GSTypedRequest<LogChallengeEventRequest_friendRequestDeclined, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_friendRequestDeclined() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "friendRequestDeclined");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_friendRequestDeclined SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_friendRequestDeclined Set_request_id( string value )
		{
			request.AddString("request_id", value);
			return this;
		}
	}
	
	public class LogEventRequest_Gameover : GSTypedRequest<LogEventRequest_Gameover, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_Gameover() : base("LogEventRequest"){
			request.AddString("eventKey", "Gameover");
		}
		public LogEventRequest_Gameover Set_Gameover( GSData value )
		{
			request.AddObject("Gameover", value);
			return this;
		}			
		
		public LogEventRequest_Gameover Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_Gameover : GSTypedRequest<LogChallengeEventRequest_Gameover, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_Gameover() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "Gameover");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_Gameover SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_Gameover Set_Gameover( GSData value )
		{
			request.AddObject("Gameover", value);
			return this;
		}
		
		public LogChallengeEventRequest_Gameover Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
	}
	
	public class LogEventRequest_GameState : GSTypedRequest<LogEventRequest_GameState, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_GameState() : base("LogEventRequest"){
			request.AddString("eventKey", "GameState");
		}
		public LogEventRequest_GameState Set_GameState( GSData value )
		{
			request.AddObject("GameState", value);
			return this;
		}			
		public LogEventRequest_GameState Set_Enpass( GSData value )
		{
			request.AddObject("Enpass", value);
			return this;
		}			
		public LogEventRequest_GameState Set_bpRed( GSData value )
		{
			request.AddObject("bpRed", value);
			return this;
		}			
		public LogEventRequest_GameState Set_bpYel( GSData value )
		{
			request.AddObject("bpYel", value);
			return this;
		}			
		public LogEventRequest_GameState Set_bpGre( GSData value )
		{
			request.AddObject("bpGre", value);
			return this;
		}			
		public LogEventRequest_GameState Set_ypBlue( GSData value )
		{
			request.AddObject("ypBlue", value);
			return this;
		}			
		public LogEventRequest_GameState Set_ypRed( GSData value )
		{
			request.AddObject("ypRed", value);
			return this;
		}			
		public LogEventRequest_GameState Set_ypGre( GSData value )
		{
			request.AddObject("ypGre", value);
			return this;
		}			
		public LogEventRequest_GameState Set_rpBlue( GSData value )
		{
			request.AddObject("rpBlue", value);
			return this;
		}			
		public LogEventRequest_GameState Set_rpYel( GSData value )
		{
			request.AddObject("rpYel", value);
			return this;
		}			
		public LogEventRequest_GameState Set_rpGre( GSData value )
		{
			request.AddObject("rpGre", value);
			return this;
		}			
		public LogEventRequest_GameState Set_gpBlue( GSData value )
		{
			request.AddObject("gpBlue", value);
			return this;
		}			
		public LogEventRequest_GameState Set_gpYel( GSData value )
		{
			request.AddObject("gpYel", value);
			return this;
		}			
		public LogEventRequest_GameState Set_gpRed( GSData value )
		{
			request.AddObject("gpRed", value);
			return this;
		}			
		public LogEventRequest_GameState Set_LastPieces( GSData value )
		{
			request.AddObject("LastPieces", value);
			return this;
		}			
		public LogEventRequest_GameState Set_LastBlue( GSData value )
		{
			request.AddObject("LastBlue", value);
			return this;
		}			
		public LogEventRequest_GameState Set_LastYellow( GSData value )
		{
			request.AddObject("LastYellow", value);
			return this;
		}			
		public LogEventRequest_GameState Set_LastRed( GSData value )
		{
			request.AddObject("LastRed", value);
			return this;
		}			
		public LogEventRequest_GameState Set_LastGreen( GSData value )
		{
			request.AddObject("LastGreen", value);
			return this;
		}			
		public LogEventRequest_GameState Set_winCondition( GSData value )
		{
			request.AddObject("winCondition", value);
			return this;
		}			
		
		public LogEventRequest_GameState Set_LastPlayer( string value )
		{
			request.AddString("LastPlayer", value);
			return this;
		}
		public LogEventRequest_GameState Set_PlayerList( GSData value )
		{
			request.AddObject("PlayerList", value);
			return this;
		}			
		
		public LogEventRequest_GameState Set_Winner( string value )
		{
			request.AddString("Winner", value);
			return this;
		}
		
		public LogEventRequest_GameState Set_WinnerName( string value )
		{
			request.AddString("WinnerName", value);
			return this;
		}
		
		public LogEventRequest_GameState Set_GameName( string value )
		{
			request.AddString("GameName", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_GameState : GSTypedRequest<LogChallengeEventRequest_GameState, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_GameState() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "GameState");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_GameState SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_GameState Set_GameState( GSData value )
		{
			request.AddObject("GameState", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_Enpass( GSData value )
		{
			request.AddObject("Enpass", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_bpRed( GSData value )
		{
			request.AddObject("bpRed", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_bpYel( GSData value )
		{
			request.AddObject("bpYel", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_bpGre( GSData value )
		{
			request.AddObject("bpGre", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_ypBlue( GSData value )
		{
			request.AddObject("ypBlue", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_ypRed( GSData value )
		{
			request.AddObject("ypRed", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_ypGre( GSData value )
		{
			request.AddObject("ypGre", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_rpBlue( GSData value )
		{
			request.AddObject("rpBlue", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_rpYel( GSData value )
		{
			request.AddObject("rpYel", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_rpGre( GSData value )
		{
			request.AddObject("rpGre", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_gpBlue( GSData value )
		{
			request.AddObject("gpBlue", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_gpYel( GSData value )
		{
			request.AddObject("gpYel", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_gpRed( GSData value )
		{
			request.AddObject("gpRed", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_LastPieces( GSData value )
		{
			request.AddObject("LastPieces", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_LastBlue( GSData value )
		{
			request.AddObject("LastBlue", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_LastYellow( GSData value )
		{
			request.AddObject("LastYellow", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_LastRed( GSData value )
		{
			request.AddObject("LastRed", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_LastGreen( GSData value )
		{
			request.AddObject("LastGreen", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_winCondition( GSData value )
		{
			request.AddObject("winCondition", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_LastPlayer( string value )
		{
			request.AddString("LastPlayer", value);
			return this;
		}
		public LogChallengeEventRequest_GameState Set_PlayerList( GSData value )
		{
			request.AddObject("PlayerList", value);
			return this;
		}
		
		public LogChallengeEventRequest_GameState Set_Winner( string value )
		{
			request.AddString("Winner", value);
			return this;
		}
		public LogChallengeEventRequest_GameState Set_WinnerName( string value )
		{
			request.AddString("WinnerName", value);
			return this;
		}
		public LogChallengeEventRequest_GameState Set_GameName( string value )
		{
			request.AddString("GameName", value);
			return this;
		}
	}
	
	public class LogEventRequest_GetMyMessages : GSTypedRequest<LogEventRequest_GetMyMessages, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_GetMyMessages() : base("LogEventRequest"){
			request.AddString("eventKey", "GetMyMessages");
		}
		public LogEventRequest_GetMyMessages Set_query( GSData value )
		{
			request.AddObject("query", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_GetMyMessages : GSTypedRequest<LogChallengeEventRequest_GetMyMessages, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_GetMyMessages() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "GetMyMessages");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_GetMyMessages SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_GetMyMessages Set_query( GSData value )
		{
			request.AddObject("query", value);
			return this;
		}
		
	}
	
	public class LogEventRequest_getPlayerFriends : GSTypedRequest<LogEventRequest_getPlayerFriends, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_getPlayerFriends() : base("LogEventRequest"){
			request.AddString("eventKey", "getPlayerFriends");
		}
		
		public LogEventRequest_getPlayerFriends Set_group( string value )
		{
			request.AddString("group", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_getPlayerFriends : GSTypedRequest<LogChallengeEventRequest_getPlayerFriends, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_getPlayerFriends() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "getPlayerFriends");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_getPlayerFriends SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_getPlayerFriends Set_group( string value )
		{
			request.AddString("group", value);
			return this;
		}
	}
	
	public class LogEventRequest_InsertEmail : GSTypedRequest<LogEventRequest_InsertEmail, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_InsertEmail() : base("LogEventRequest"){
			request.AddString("eventKey", "InsertEmail");
		}
		
		public LogEventRequest_InsertEmail Set_stringOne( string value )
		{
			request.AddString("stringOne", value);
			return this;
		}
		
		public LogEventRequest_InsertEmail Set_stringTwo( string value )
		{
			request.AddString("stringTwo", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_InsertEmail : GSTypedRequest<LogChallengeEventRequest_InsertEmail, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_InsertEmail() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "InsertEmail");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_InsertEmail SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_InsertEmail Set_stringOne( string value )
		{
			request.AddString("stringOne", value);
			return this;
		}
		public LogChallengeEventRequest_InsertEmail Set_stringTwo( string value )
		{
			request.AddString("stringTwo", value);
			return this;
		}
	}
	
	public class LogEventRequest_MakeRightTurn : GSTypedRequest<LogEventRequest_MakeRightTurn, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_MakeRightTurn() : base("LogEventRequest"){
			request.AddString("eventKey", "MakeRightTurn");
		}
		
		public LogEventRequest_MakeRightTurn Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogEventRequest_MakeRightTurn Set_wincon( GSData value )
		{
			request.AddObject("wincon", value);
			return this;
		}			
		public LogEventRequest_MakeRightTurn Set_players( GSData value )
		{
			request.AddObject("players", value);
			return this;
		}			
		
		public LogEventRequest_MakeRightTurn Set_skipto( string value )
		{
			request.AddString("skipto", value);
			return this;
		}
		
		public LogEventRequest_MakeRightTurn Set_skipfrom( string value )
		{
			request.AddString("skipfrom", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_MakeRightTurn : GSTypedRequest<LogChallengeEventRequest_MakeRightTurn, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_MakeRightTurn() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "MakeRightTurn");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_MakeRightTurn SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_MakeRightTurn Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogChallengeEventRequest_MakeRightTurn Set_wincon( GSData value )
		{
			request.AddObject("wincon", value);
			return this;
		}
		
		public LogChallengeEventRequest_MakeRightTurn Set_players( GSData value )
		{
			request.AddObject("players", value);
			return this;
		}
		
		public LogChallengeEventRequest_MakeRightTurn Set_skipto( string value )
		{
			request.AddString("skipto", value);
			return this;
		}
		public LogChallengeEventRequest_MakeRightTurn Set_skipfrom( string value )
		{
			request.AddString("skipfrom", value);
			return this;
		}
	}
	
	public class LogEventRequest_MakeThisTurn : GSTypedRequest<LogEventRequest_MakeThisTurn, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_MakeThisTurn() : base("LogEventRequest"){
			request.AddString("eventKey", "MakeThisTurn");
		}
		
		public LogEventRequest_MakeThisTurn Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogEventRequest_MakeThisTurn Set_wincon( GSData value )
		{
			request.AddObject("wincon", value);
			return this;
		}			
		public LogEventRequest_MakeThisTurn Set_players( GSData value )
		{
			request.AddObject("players", value);
			return this;
		}			
		
		public LogEventRequest_MakeThisTurn Set_skipto( string value )
		{
			request.AddString("skipto", value);
			return this;
		}
		
		public LogEventRequest_MakeThisTurn Set_skipfrom( string value )
		{
			request.AddString("skipfrom", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_MakeThisTurn : GSTypedRequest<LogChallengeEventRequest_MakeThisTurn, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_MakeThisTurn() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "MakeThisTurn");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_MakeThisTurn SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_MakeThisTurn Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogChallengeEventRequest_MakeThisTurn Set_wincon( GSData value )
		{
			request.AddObject("wincon", value);
			return this;
		}
		
		public LogChallengeEventRequest_MakeThisTurn Set_players( GSData value )
		{
			request.AddObject("players", value);
			return this;
		}
		
		public LogChallengeEventRequest_MakeThisTurn Set_skipto( string value )
		{
			request.AddString("skipto", value);
			return this;
		}
		public LogChallengeEventRequest_MakeThisTurn Set_skipfrom( string value )
		{
			request.AddString("skipfrom", value);
			return this;
		}
	}
	
	public class LogEventRequest_PlayerExpire : GSTypedRequest<LogEventRequest_PlayerExpire, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_PlayerExpire() : base("LogEventRequest"){
			request.AddString("eventKey", "PlayerExpire");
		}
		
		public LogEventRequest_PlayerExpire Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogEventRequest_PlayerExpire Set_activePlayerList( GSData value )
		{
			request.AddObject("activePlayerList", value);
			return this;
		}			
		
		public LogEventRequest_PlayerExpire Set_NextPlayerColor( string value )
		{
			request.AddString("NextPlayerColor", value);
			return this;
		}
		
		public LogEventRequest_PlayerExpire Set_action( string value )
		{
			request.AddString("action", value);
			return this;
		}
		
		public LogEventRequest_PlayerExpire Set_livePlayer( string value )
		{
			request.AddString("livePlayer", value);
			return this;
		}
		public LogEventRequest_PlayerExpire Set_ConsecutiveSkips( long value )
		{
			request.AddNumber("ConsecutiveSkips", value);
			return this;
		}			
		public LogEventRequest_PlayerExpire Set_PlayersToSkip( GSData value )
		{
			request.AddObject("PlayersToSkip", value);
			return this;
		}			
		
		public LogEventRequest_PlayerExpire Set_GameName( string value )
		{
			request.AddString("GameName", value);
			return this;
		}
		public LogEventRequest_PlayerExpire Set_ExpireTime( long value )
		{
			request.AddNumber("ExpireTime", value);
			return this;
		}			
		
		public LogEventRequest_PlayerExpire Set_Token( string value )
		{
			request.AddString("Token", value);
			return this;
		}
		public LogEventRequest_PlayerExpire Set_wincon( GSData value )
		{
			request.AddObject("wincon", value);
			return this;
		}			
		public LogEventRequest_PlayerExpire Set_PlayerList( GSData value )
		{
			request.AddObject("PlayerList", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_PlayerExpire : GSTypedRequest<LogChallengeEventRequest_PlayerExpire, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_PlayerExpire() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "PlayerExpire");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_PlayerExpire SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_PlayerExpire Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogChallengeEventRequest_PlayerExpire Set_activePlayerList( GSData value )
		{
			request.AddObject("activePlayerList", value);
			return this;
		}
		
		public LogChallengeEventRequest_PlayerExpire Set_NextPlayerColor( string value )
		{
			request.AddString("NextPlayerColor", value);
			return this;
		}
		public LogChallengeEventRequest_PlayerExpire Set_action( string value )
		{
			request.AddString("action", value);
			return this;
		}
		public LogChallengeEventRequest_PlayerExpire Set_livePlayer( string value )
		{
			request.AddString("livePlayer", value);
			return this;
		}
		public LogChallengeEventRequest_PlayerExpire Set_ConsecutiveSkips( long value )
		{
			request.AddNumber("ConsecutiveSkips", value);
			return this;
		}			
		public LogChallengeEventRequest_PlayerExpire Set_PlayersToSkip( GSData value )
		{
			request.AddObject("PlayersToSkip", value);
			return this;
		}
		
		public LogChallengeEventRequest_PlayerExpire Set_GameName( string value )
		{
			request.AddString("GameName", value);
			return this;
		}
		public LogChallengeEventRequest_PlayerExpire Set_ExpireTime( long value )
		{
			request.AddNumber("ExpireTime", value);
			return this;
		}			
		public LogChallengeEventRequest_PlayerExpire Set_Token( string value )
		{
			request.AddString("Token", value);
			return this;
		}
		public LogChallengeEventRequest_PlayerExpire Set_wincon( GSData value )
		{
			request.AddObject("wincon", value);
			return this;
		}
		
		public LogChallengeEventRequest_PlayerExpire Set_PlayerList( GSData value )
		{
			request.AddObject("PlayerList", value);
			return this;
		}
		
	}
	
	public class LogEventRequest_PlayerQuit : GSTypedRequest<LogEventRequest_PlayerQuit, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_PlayerQuit() : base("LogEventRequest"){
			request.AddString("eventKey", "PlayerQuit");
		}
		
		public LogEventRequest_PlayerQuit Set_color( string value )
		{
			request.AddString("color", value);
			return this;
		}
		
		public LogEventRequest_PlayerQuit Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogEventRequest_PlayerQuit Set_winCondition( GSData value )
		{
			request.AddObject("winCondition", value);
			return this;
		}			
		public LogEventRequest_PlayerQuit Set_PlayerList( GSData value )
		{
			request.AddObject("PlayerList", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_PlayerQuit : GSTypedRequest<LogChallengeEventRequest_PlayerQuit, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_PlayerQuit() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "PlayerQuit");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_PlayerQuit SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_PlayerQuit Set_color( string value )
		{
			request.AddString("color", value);
			return this;
		}
		public LogChallengeEventRequest_PlayerQuit Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogChallengeEventRequest_PlayerQuit Set_winCondition( GSData value )
		{
			request.AddObject("winCondition", value);
			return this;
		}
		
		public LogChallengeEventRequest_PlayerQuit Set_PlayerList( GSData value )
		{
			request.AddObject("PlayerList", value);
			return this;
		}
		
	}
	
	public class LogEventRequest_PlayerStatus : GSTypedRequest<LogEventRequest_PlayerStatus, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_PlayerStatus() : base("LogEventRequest"){
			request.AddString("eventKey", "PlayerStatus");
		}
		
		public LogEventRequest_PlayerStatus Set_Status( string value )
		{
			request.AddString("Status", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_PlayerStatus : GSTypedRequest<LogChallengeEventRequest_PlayerStatus, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_PlayerStatus() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "PlayerStatus");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_PlayerStatus SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_PlayerStatus Set_Status( string value )
		{
			request.AddString("Status", value);
			return this;
		}
	}
	
	public class LogEventRequest_PrivateChat : GSTypedRequest<LogEventRequest_PrivateChat, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_PrivateChat() : base("LogEventRequest"){
			request.AddString("eventKey", "PrivateChat");
		}
		
		public LogEventRequest_PrivateChat Set_msg( string value )
		{
			request.AddString("msg", value);
			return this;
		}
		
		public LogEventRequest_PrivateChat Set_senderID( string value )
		{
			request.AddString("senderID", value);
			return this;
		}
		
		public LogEventRequest_PrivateChat Set_recipientID( string value )
		{
			request.AddString("recipientID", value);
			return this;
		}
		
		public LogEventRequest_PrivateChat Set_senderColor( string value )
		{
			request.AddString("senderColor", value);
			return this;
		}
		
		public LogEventRequest_PrivateChat Set_recipientColor( string value )
		{
			request.AddString("recipientColor", value);
			return this;
		}
		
		public LogEventRequest_PrivateChat Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_PrivateChat : GSTypedRequest<LogChallengeEventRequest_PrivateChat, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_PrivateChat() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "PrivateChat");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_PrivateChat SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_PrivateChat Set_msg( string value )
		{
			request.AddString("msg", value);
			return this;
		}
		public LogChallengeEventRequest_PrivateChat Set_senderID( string value )
		{
			request.AddString("senderID", value);
			return this;
		}
		public LogChallengeEventRequest_PrivateChat Set_recipientID( string value )
		{
			request.AddString("recipientID", value);
			return this;
		}
		public LogChallengeEventRequest_PrivateChat Set_senderColor( string value )
		{
			request.AddString("senderColor", value);
			return this;
		}
		public LogChallengeEventRequest_PrivateChat Set_recipientColor( string value )
		{
			request.AddString("recipientColor", value);
			return this;
		}
		public LogChallengeEventRequest_PrivateChat Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
	}
	
	public class LogEventRequest_pubgame : GSTypedRequest<LogEventRequest_pubgame, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_pubgame() : base("LogEventRequest"){
			request.AddString("eventKey", "pubgame");
		}
		
		public LogEventRequest_pubgame Set_players( string value )
		{
			request.AddString("players", value);
			return this;
		}
		
		public LogEventRequest_pubgame Set_Chalid( string value )
		{
			request.AddString("Chalid", value);
			return this;
		}
		
		public LogEventRequest_pubgame Set_GameName( string value )
		{
			request.AddString("GameName", value);
			return this;
		}
		public LogEventRequest_pubgame Set_TurnLimit( long value )
		{
			request.AddNumber("TurnLimit", value);
			return this;
		}			
		public LogEventRequest_pubgame Set_playersData( GSData value )
		{
			request.AddObject("playersData", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_pubgame : GSTypedRequest<LogChallengeEventRequest_pubgame, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_pubgame() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "pubgame");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_pubgame SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_pubgame Set_players( string value )
		{
			request.AddString("players", value);
			return this;
		}
		public LogChallengeEventRequest_pubgame Set_Chalid( string value )
		{
			request.AddString("Chalid", value);
			return this;
		}
		public LogChallengeEventRequest_pubgame Set_GameName( string value )
		{
			request.AddString("GameName", value);
			return this;
		}
		public LogChallengeEventRequest_pubgame Set_TurnLimit( long value )
		{
			request.AddNumber("TurnLimit", value);
			return this;
		}			
		public LogChallengeEventRequest_pubgame Set_playersData( GSData value )
		{
			request.AddObject("playersData", value);
			return this;
		}
		
	}
	
	public class LogEventRequest_RemoveScriptData : GSTypedRequest<LogEventRequest_RemoveScriptData, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_RemoveScriptData() : base("LogEventRequest"){
			request.AddString("eventKey", "RemoveScriptData");
		}
		
		public LogEventRequest_RemoveScriptData Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		
		public LogEventRequest_RemoveScriptData Set_data( string value )
		{
			request.AddString("data", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_RemoveScriptData : GSTypedRequest<LogChallengeEventRequest_RemoveScriptData, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_RemoveScriptData() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "RemoveScriptData");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_RemoveScriptData SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_RemoveScriptData Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
		public LogChallengeEventRequest_RemoveScriptData Set_data( string value )
		{
			request.AddString("data", value);
			return this;
		}
	}
	
	public class LogEventRequest_SignOut : GSTypedRequest<LogEventRequest_SignOut, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_SignOut() : base("LogEventRequest"){
			request.AddString("eventKey", "SignOut");
		}
	}
	
	public class LogChallengeEventRequest_SignOut : GSTypedRequest<LogChallengeEventRequest_SignOut, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_SignOut() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "SignOut");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_SignOut SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_SkipPlayer : GSTypedRequest<LogEventRequest_SkipPlayer, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_SkipPlayer() : base("LogEventRequest"){
			request.AddString("eventKey", "SkipPlayer");
		}
		
		public LogEventRequest_SkipPlayer Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_SkipPlayer : GSTypedRequest<LogChallengeEventRequest_SkipPlayer, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_SkipPlayer() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "SkipPlayer");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_SkipPlayer SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_SkipPlayer Set_cid( string value )
		{
			request.AddString("cid", value);
			return this;
		}
	}
	
	public class LogEventRequest_TurnState_Blue : GSTypedRequest<LogEventRequest_TurnState_Blue, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_TurnState_Blue() : base("LogEventRequest"){
			request.AddString("eventKey", "TurnState_Blue");
		}
		public LogEventRequest_TurnState_Blue Set_before( GSData value )
		{
			request.AddObject("before", value);
			return this;
		}			
		public LogEventRequest_TurnState_Blue Set_after( GSData value )
		{
			request.AddObject("after", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_TurnState_Blue : GSTypedRequest<LogChallengeEventRequest_TurnState_Blue, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_TurnState_Blue() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "TurnState_Blue");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_TurnState_Blue SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_TurnState_Blue Set_before( GSData value )
		{
			request.AddObject("before", value);
			return this;
		}
		
		public LogChallengeEventRequest_TurnState_Blue Set_after( GSData value )
		{
			request.AddObject("after", value);
			return this;
		}
		
	}
	
	public class LogEventRequest_TurnState_Green : GSTypedRequest<LogEventRequest_TurnState_Green, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_TurnState_Green() : base("LogEventRequest"){
			request.AddString("eventKey", "TurnState_Green");
		}
		
		public LogEventRequest_TurnState_Green Set_before( string value )
		{
			request.AddString("before", value);
			return this;
		}
		
		public LogEventRequest_TurnState_Green Set_after( string value )
		{
			request.AddString("after", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_TurnState_Green : GSTypedRequest<LogChallengeEventRequest_TurnState_Green, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_TurnState_Green() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "TurnState_Green");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_TurnState_Green SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_TurnState_Green Set_before( string value )
		{
			request.AddString("before", value);
			return this;
		}
		public LogChallengeEventRequest_TurnState_Green Set_after( string value )
		{
			request.AddString("after", value);
			return this;
		}
	}
	
	public class LogEventRequest_TurnState_Red : GSTypedRequest<LogEventRequest_TurnState_Red, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_TurnState_Red() : base("LogEventRequest"){
			request.AddString("eventKey", "TurnState_Red");
		}
		public LogEventRequest_TurnState_Red Set_before( GSData value )
		{
			request.AddObject("before", value);
			return this;
		}			
		public LogEventRequest_TurnState_Red Set_after( GSData value )
		{
			request.AddObject("after", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_TurnState_Red : GSTypedRequest<LogChallengeEventRequest_TurnState_Red, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_TurnState_Red() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "TurnState_Red");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_TurnState_Red SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_TurnState_Red Set_before( GSData value )
		{
			request.AddObject("before", value);
			return this;
		}
		
		public LogChallengeEventRequest_TurnState_Red Set_after( GSData value )
		{
			request.AddObject("after", value);
			return this;
		}
		
	}
	
	public class LogEventRequest_TurnState_Yellow : GSTypedRequest<LogEventRequest_TurnState_Yellow, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_TurnState_Yellow() : base("LogEventRequest"){
			request.AddString("eventKey", "TurnState_Yellow");
		}
		public LogEventRequest_TurnState_Yellow Set_before( GSData value )
		{
			request.AddObject("before", value);
			return this;
		}			
		public LogEventRequest_TurnState_Yellow Set_after( GSData value )
		{
			request.AddObject("after", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_TurnState_Yellow : GSTypedRequest<LogChallengeEventRequest_TurnState_Yellow, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_TurnState_Yellow() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "TurnState_Yellow");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_TurnState_Yellow SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_TurnState_Yellow Set_before( GSData value )
		{
			request.AddObject("before", value);
			return this;
		}
		
		public LogChallengeEventRequest_TurnState_Yellow Set_after( GSData value )
		{
			request.AddObject("after", value);
			return this;
		}
		
	}
	
	public class LogEventRequest_UpdateEmail : GSTypedRequest<LogEventRequest_UpdateEmail, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_UpdateEmail() : base("LogEventRequest"){
			request.AddString("eventKey", "UpdateEmail");
		}
		
		public LogEventRequest_UpdateEmail Set_stringOne( string value )
		{
			request.AddString("stringOne", value);
			return this;
		}
		
		public LogEventRequest_UpdateEmail Set_stringTwo( string value )
		{
			request.AddString("stringTwo", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_UpdateEmail : GSTypedRequest<LogChallengeEventRequest_UpdateEmail, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_UpdateEmail() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "UpdateEmail");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_UpdateEmail SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_UpdateEmail Set_stringOne( string value )
		{
			request.AddString("stringOne", value);
			return this;
		}
		public LogChallengeEventRequest_UpdateEmail Set_stringTwo( string value )
		{
			request.AddString("stringTwo", value);
			return this;
		}
	}
	
	public class LogEventRequest_UpdateName : GSTypedRequest<LogEventRequest_UpdateName, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_UpdateName() : base("LogEventRequest"){
			request.AddString("eventKey", "UpdateName");
		}
		
		public LogEventRequest_UpdateName Set_stringOne( string value )
		{
			request.AddString("stringOne", value);
			return this;
		}
		
		public LogEventRequest_UpdateName Set_stringTwo( string value )
		{
			request.AddString("stringTwo", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_UpdateName : GSTypedRequest<LogChallengeEventRequest_UpdateName, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_UpdateName() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "UpdateName");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_UpdateName SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_UpdateName Set_stringOne( string value )
		{
			request.AddString("stringOne", value);
			return this;
		}
		public LogChallengeEventRequest_UpdateName Set_stringTwo( string value )
		{
			request.AddString("stringTwo", value);
			return this;
		}
	}
	
}
	

namespace GameSparks.Api.Messages {

		public class ScriptMessage_DrawVote : ScriptMessage {
		
			public new static Action<ScriptMessage_DrawVote> Listener;
	
			public ScriptMessage_DrawVote(GSData data) : base(data){}
	
			private static ScriptMessage_DrawVote Create(GSData data)
			{
				ScriptMessage_DrawVote message = new ScriptMessage_DrawVote (data);
				return message;
			}
	
			static ScriptMessage_DrawVote()
			{
				handlers.Add (".ScriptMessage_DrawVote", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_GameWinner : ScriptMessage {
		
			public new static Action<ScriptMessage_GameWinner> Listener;
	
			public ScriptMessage_GameWinner(GSData data) : base(data){}
	
			private static ScriptMessage_GameWinner Create(GSData data)
			{
				ScriptMessage_GameWinner message = new ScriptMessage_GameWinner (data);
				return message;
			}
	
			static ScriptMessage_GameWinner()
			{
				handlers.Add (".ScriptMessage_GameWinner", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_GetMyMessages : ScriptMessage {
		
			public new static Action<ScriptMessage_GetMyMessages> Listener;
	
			public ScriptMessage_GetMyMessages(GSData data) : base(data){}
	
			private static ScriptMessage_GetMyMessages Create(GSData data)
			{
				ScriptMessage_GetMyMessages message = new ScriptMessage_GetMyMessages (data);
				return message;
			}
	
			static ScriptMessage_GetMyMessages()
			{
				handlers.Add (".ScriptMessage_GetMyMessages", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_MoveExpiredPlayer : ScriptMessage {
		
			public new static Action<ScriptMessage_MoveExpiredPlayer> Listener;
	
			public ScriptMessage_MoveExpiredPlayer(GSData data) : base(data){}
	
			private static ScriptMessage_MoveExpiredPlayer Create(GSData data)
			{
				ScriptMessage_MoveExpiredPlayer message = new ScriptMessage_MoveExpiredPlayer (data);
				return message;
			}
	
			static ScriptMessage_MoveExpiredPlayer()
			{
				handlers.Add (".ScriptMessage_MoveExpiredPlayer", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_PlayerQuit : ScriptMessage {
		
			public new static Action<ScriptMessage_PlayerQuit> Listener;
	
			public ScriptMessage_PlayerQuit(GSData data) : base(data){}
	
			private static ScriptMessage_PlayerQuit Create(GSData data)
			{
				ScriptMessage_PlayerQuit message = new ScriptMessage_PlayerQuit (data);
				return message;
			}
	
			static ScriptMessage_PlayerQuit()
			{
				handlers.Add (".ScriptMessage_PlayerQuit", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_PrivateChat : ScriptMessage {
		
			public new static Action<ScriptMessage_PrivateChat> Listener;
	
			public ScriptMessage_PrivateChat(GSData data) : base(data){}
	
			private static ScriptMessage_PrivateChat Create(GSData data)
			{
				ScriptMessage_PrivateChat message = new ScriptMessage_PrivateChat (data);
				return message;
			}
	
			static ScriptMessage_PrivateChat()
			{
				handlers.Add (".ScriptMessage_PrivateChat", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_Skip : ScriptMessage {
		
			public new static Action<ScriptMessage_Skip> Listener;
	
			public ScriptMessage_Skip(GSData data) : base(data){}
	
			private static ScriptMessage_Skip Create(GSData data)
			{
				ScriptMessage_Skip message = new ScriptMessage_Skip (data);
				return message;
			}
	
			static ScriptMessage_Skip()
			{
				handlers.Add (".ScriptMessage_Skip", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_friendAcceptedMessage : ScriptMessage {
		
			public new static Action<ScriptMessage_friendAcceptedMessage> Listener;
	
			public ScriptMessage_friendAcceptedMessage(GSData data) : base(data){}
	
			private static ScriptMessage_friendAcceptedMessage Create(GSData data)
			{
				ScriptMessage_friendAcceptedMessage message = new ScriptMessage_friendAcceptedMessage (data);
				return message;
			}
	
			static ScriptMessage_friendAcceptedMessage()
			{
				handlers.Add (".ScriptMessage_friendAcceptedMessage", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_friendRequestDenied : ScriptMessage {
		
			public new static Action<ScriptMessage_friendRequestDenied> Listener;
	
			public ScriptMessage_friendRequestDenied(GSData data) : base(data){}
	
			private static ScriptMessage_friendRequestDenied Create(GSData data)
			{
				ScriptMessage_friendRequestDenied message = new ScriptMessage_friendRequestDenied (data);
				return message;
			}
	
			static ScriptMessage_friendRequestDenied()
			{
				handlers.Add (".ScriptMessage_friendRequestDenied", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_friendRequestMessage : ScriptMessage {
		
			public new static Action<ScriptMessage_friendRequestMessage> Listener;
	
			public ScriptMessage_friendRequestMessage(GSData data) : base(data){}
	
			private static ScriptMessage_friendRequestMessage Create(GSData data)
			{
				ScriptMessage_friendRequestMessage message = new ScriptMessage_friendRequestMessage (data);
				return message;
			}
	
			static ScriptMessage_friendRequestMessage()
			{
				handlers.Add (".ScriptMessage_friendRequestMessage", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}
		public class ScriptMessage_pubgame : ScriptMessage {
		
			public new static Action<ScriptMessage_pubgame> Listener;
	
			public ScriptMessage_pubgame(GSData data) : base(data){}
	
			private static ScriptMessage_pubgame Create(GSData data)
			{
				ScriptMessage_pubgame message = new ScriptMessage_pubgame (data);
				return message;
			}
	
			static ScriptMessage_pubgame()
			{
				handlers.Add (".ScriptMessage_pubgame", Create);
	
			}
			
			override public void NotifyListeners()
			{
				if (Listener != null)
				{
					Listener (this);
				}
			}
		}

}
