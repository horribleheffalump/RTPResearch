#include "stdafx.h"

HANDLE myNplParser = INVALID_HANDLE_VALUE;
HANDLE myFrameParserConfig = INVALID_HANDLE_VALUE;

ULONG myHTTPFilterID = 0;    // Global for the Filter.
ULONG myHTTPFieldID = 0;     // Global ID for the HTTP data field.

struct SSRTStruct {
	WCHAR SSRTId[11];
	WCHAR Source[255];
	WCHAR Destination[255];
	bool isAudio = false;
	UINT64 TimeStampReceiverFirst = 0;
	UINT64 TimeStampReceiverLast = 0;
	UINT64 TimeStampSenderFirst = 0;
	UINT64 TimeStampSenderLast = 0;
	double TimeStampReceiverDouble = 0;
	double TimeStampSenderDouble = 0;
	WCHAR filename[1024];
	FILE *sessionsStream;
	double rate = 0;
	double rateReal = 0;
	UINT64 rateSamples = 0;
};

void __stdcall
MyParserBuild(PVOID Context, ULONG StatusCode, LPCWSTR lpDescription, ULONG ErrorType)
{
	//wprintf(L"%s\n", lpDescription);
}

// Returns a frame parser with a filter and one data field.
// INVALID_HANDLE_VALUE indicates failure.
//HANDLE
//MyLoadNPL(void)
//{
//	HANDLE myFrameParser = INVALID_HANDLE_VALUE;
//	ULONG ret;
//
//	// Use NULL to load default NPL set.
//	ret = NmLoadNplParser(NULL, NmAppendRegisteredNplSets, MyParserBuild, 0, &myNplParser);
//
//	if (ret == ERROR_SUCCESS){
//		ret = NmCreateFrameParserConfiguration(myNplParser, MyParserBuild, 0, &myFrameParserConfig);
//
//		if (ret == ERROR_SUCCESS)
//		{
//
//			ret = NmAddFilter(myFrameParserConfig, L"RTP", &myHTTPFilterID);
//			if (ret != 0)
//			{
//				wprintf(L"Failed to add fitler, error 0x%X\n", ret);
//			}
//
//			ret = NmAddField(myFrameParserConfig, L"Rtp", &myHTTPFieldID);
//			if (ret != ERROR_SUCCESS)
//			{
//				wprintf(L"Failed to add field, error 0x%X\n", ret);
//			}
//
//			ret = NmCreateFrameParser(myFrameParserConfig, &myFrameParser);
//
//			if (ret != ERROR_SUCCESS)
//			{
//				wprintf(L"Failed to create frame parser, error 0x%X\n", ret);
//				NmCloseHandle(myFrameParserConfig);
//				NmCloseHandle(myNplParser);
//				return INVALID_HANDLE_VALUE;
//			}
//		}
//		else
//		{
//			wprintf(L"Unable to load parser config, error 0x%X\n", ret);
//			NmCloseHandle(myNplParser);
//			return INVALID_HANDLE_VALUE;
//		}
//
//	}
//	else
//	{
//		wprintf(L"Unable to load NPL\n");
//		return INVALID_HANDLE_VALUE;
//	}
//
//	return(myFrameParser);
//}

HANDLE
MyLoadNPL(void)
{
	HANDLE myFrameParser = INVALID_HANDLE_VALUE;
	ULONG ret;

	// Use NULL to load the default NPL set.
	ret = NmLoadNplParser(NULL, NmAppendRegisteredNplSets, MyParserBuild, 0, &myNplParser);

	if (ret == ERROR_SUCCESS)
	{
		ret = NmCreateFrameParserConfiguration(myNplParser, MyParserBuild, 0, &myFrameParserConfig);

		if (ret == ERROR_SUCCESS)
		{
			// Turn off optimizations to get display strings for all fields.
			ret = NmCreateFrameParser(myFrameParserConfig, &myFrameParser, NmParserOptimizeNone);

			if (ret != ERROR_SUCCESS)
			{
				wprintf(L"Failed to create frame parser, error 0x%X\n", ret);
				NmCloseHandle(myFrameParserConfig);
				NmCloseHandle(myNplParser);
				return INVALID_HANDLE_VALUE;
			}
		}
		else
		{
			wprintf(L"Unable to load parser config, error 0x%X\n", ret);
			NmCloseHandle(myNplParser);
			return INVALID_HANDLE_VALUE;
		}
	}
	else
	{
		wprintf(L"Unable to load NPL\n");
		return INVALID_HANDLE_VALUE;
	}

	return(myFrameParser);
}

void
UnLoadNPL(void)
{
	NmCloseHandle(myNplParser);
	NmCloseHandle(myFrameParserConfig);
}

SSRTStruct SSRCs[25];
int SSRCsCount = 0;


void FindAllSSRCs(WCHAR *filename)
{
	ULONG ret = ERROR_SUCCESS;
	// Open the specified capture file.
	HANDLE myCaptureFile = INVALID_HANDLE_VALUE;
	if (ERROR_SUCCESS == NmOpenCaptureFile(filename, &myCaptureFile))
	{
		// Initialize the parser engine and return a frame parser.
		HANDLE myFrameParser = MyLoadNPL();
		if (myFrameParser != INVALID_HANDLE_VALUE)
		{
			ULONG FrameNumberTo = 0;
			ret = NmGetFrameCount(myCaptureFile, &FrameNumberTo);
			if (ret == ERROR_SUCCESS)
			{
				HANDLE myRawFrame = INVALID_HANDLE_VALUE;

				for (ULONG fNum = 0; fNum < FrameNumberTo; fNum++)
				{
					HANDLE myParsedFrame = INVALID_HANDLE_VALUE;
					ret = NmGetFrame(myCaptureFile, fNum, &myRawFrame);

					if (ret == ERROR_SUCCESS)
					{
						// The last parameter is for the API to return
						// the reassembled frame if enabled.
						// NULL indicates that the API discards the
						// reassembled frame.

						ret = NmParseFrame(myFrameParser, myRawFrame, fNum, NmFieldDisplayStringRequired, &myParsedFrame, NULL);
						if (ret == ERROR_SUCCESS)
						{
							//start field parce

							ULONG myFieldCount;
							ret = NmGetFieldCount(myParsedFrame, &myFieldCount);
							if (ret != ERROR_SUCCESS)
							{
								wprintf(L"Error getting field count, ret = %d\n", ret);
								//return ret;
								continue;
							}
							else
							{
								// Keeps track of a negative offset so protocols start at zero.
								int IndentOffset = 0;
								WCHAR *SyncSourceIdStr = (WCHAR *)malloc(11 * sizeof(WCHAR));
								WCHAR *TimestampStr = (WCHAR *)malloc(11 * sizeof(WCHAR));
								UINT64 TimeStampSource = 0;
								bool foundRTPdata = false;

								// Iterate through all the fields in this frame.
								for (ULONG j = 0; j < myFieldCount; j++)
								{
									NM_PARSED_FIELD_INFO myParsedDataFieldInfo;
									myParsedDataFieldInfo.Size = sizeof(NM_PARSED_FIELD_INFO);

									// Call with Display String Required as the only option.
									ret = NmGetParsedFieldInfo(myParsedFrame,
										j,
										NmFieldDisplayStringRequired,
										&myParsedDataFieldInfo);

									if (j > 0 && myParsedDataFieldInfo.ValueType == 0)
									{
										IndentOffset--;
									}

									// Allocate space for the field name and get it.
									ULONG FieldNameLength = (myParsedDataFieldInfo.NamePathLength + 1) * sizeof(WCHAR);
									WCHAR *FieldName = (WCHAR *)malloc(FieldNameLength);
									ret = NmGetFieldName(myParsedFrame, j, NmFieldNamePath, FieldNameLength, FieldName);

									if (!wcscmp(FieldName, L"Timestamp") ||
										!wcscmp(FieldName, L"SyncSourceId") ||
										!wcscmp(FieldName, L"SourceAddress") ||
										!wcscmp(FieldName, L"DestinationAddress"))
									{
										foundRTPdata = true;
										// Allocate space for the display string and get it.
										ULONG DisplayFormatLength = (myParsedDataFieldInfo.DisplayStringLength + 1) * sizeof(WCHAR);
										WCHAR *DisplayFormat = (WCHAR *)malloc(DisplayFormatLength);
										ret = NmGetFieldName(myParsedFrame, j, NmFieldDisplayString, DisplayFormatLength, DisplayFormat);
										if (!wcscmp(FieldName, L"Timestamp"))
										{
											int k = 0;
											for (k = 0; k < 10; k++)
											{
												TimestampStr[k] = DisplayFormat[k];
												if (DisplayFormat[k + 1] == ' ') break;
											}
											TimestampStr[k + 1] = 0;

											TimeStampSource = _wtoi64(TimestampStr);
											//wprintf(L"%s ", DisplayFormatCut);
										}
										if (!wcscmp(FieldName, L"SourceAddress"))
											{
												wcscpy_s(SSRCs[SSRCsCount].Source, DisplayFormat);
											}

											if (!wcscmp(FieldName, L"DestinationAddress"))
											{
												wcscpy_s(SSRCs[SSRCsCount].Destination, DisplayFormat);
											}


											if (!wcscmp(FieldName, L"SyncSourceId"))
											{

												int k = 0;
												for (k = 0; k < 10; k++)
												{
													SyncSourceIdStr[k] = DisplayFormat[k];
													if (DisplayFormat[k + 1] == ' ') break;
												}
												SyncSourceIdStr[k + 1] = 0;

												bool found = false;
												for (int jj = 0; jj < SSRCsCount; jj++)
												{
													if (!wcscmp(SSRCs[jj].SSRTId, SyncSourceIdStr))
													{
														found = true;
														break;
													}
												}
												if (!found)
												{
													wcscpy_s(SSRCs[SSRCsCount].SSRTId, SyncSourceIdStr);
													SSRCsCount++;
												}

												//wprintf(L"%s ", DisplayFormatCut);
											}
										free(DisplayFormat);
									}
									free(FieldName);
								}
								UINT64 TimeStamp = 0;
								ret = NmGetFrameTimeStamp(myRawFrame, &TimeStamp);

								UINT64 TimeStampHost = 0;


								if (foundRTPdata)
								{
									for (int ssrcn = 0; ssrcn < SSRCsCount; ssrcn++)
									{
										if (wcscmp(SyncSourceIdStr, SSRCs[ssrcn].SSRTId) == 0)
										{
											//if (SSRCs[ssrcn].TimeStampSenderFirst != 0)
											//{
											//	UINT64 TimeStampSenderPrevious = SSRCs[ssrcn].TimeStampSenderLast + SSRCs[ssrcn].TimeStampSenderFirst;
											//	UINT64 TimeStampReceiverPrevious = SSRCs[ssrcn].TimeStampReceiverLast + SSRCs[ssrcn].TimeStampReceiverFirst;
											//	if ((TimeStamp - TimeStampReceiverPrevious > 0) && (TimeStampSource - TimeStampSenderPrevious > 0))
											//	{
											//		double rateSample = double(TimeStampSource - TimeStampSenderPrevious) * 1000 / double (TimeStamp - TimeStampReceiverPrevious);
											//		SSRCs[ssrcn].rate = SSRCs[ssrcn].rate * (SSRCs[ssrcn].rateSamples / (SSRCs[ssrcn].rateSamples + 1)) + rateSample / (SSRCs[ssrcn].rateSamples + 1);
											//		SSRCs[ssrcn].rateSamples++;
											//	}
											//}

											//int VIDEOframerateKHz = 90;

											if (SSRCs[ssrcn].TimeStampSenderFirst == 0) SSRCs[ssrcn].TimeStampSenderFirst = TimeStampSource;
											SSRCs[ssrcn].TimeStampSenderLast = TimeStampSource - SSRCs[ssrcn].TimeStampSenderFirst;
											if (SSRCs[ssrcn].TimeStampReceiverFirst == 0) SSRCs[ssrcn].TimeStampReceiverFirst = TimeStamp;
											SSRCs[ssrcn].TimeStampReceiverLast = TimeStamp - SSRCs[ssrcn].TimeStampReceiverFirst;

											SSRCs[ssrcn].TimeStampReceiverDouble = SSRCs[ssrcn].TimeStampReceiverLast / double(10000000);
											SSRCs[ssrcn].TimeStampSenderDouble = SSRCs[ssrcn].TimeStampSenderLast / double(1000); // / VIDEOframerateKHz;
										}
									}
								}
							}
							NmCloseHandle(myParsedFrame);
						}
						else
						{
							// Print an error, but continue to loop.
							wprintf(L"Error: 0x%X, trying to parse frame %d\n", ret, fNum + 1);
						}

						// Release the current raw frame.
						NmCloseHandle(myRawFrame);
					}
					else
					{
						// Print an error, but continue to loop.
						wprintf(L"Errors getting raw frame %d\n", fNum + 1);
					}
				}
			}
			NmCloseHandle(myFrameParser);

		}
		else
		{
			wprintf(L"Errors creating frame parser\n");
		}

		NmCloseHandle(myCaptureFile);
	}
	else
	{
		wprintf(L"Errors openning capture file\n");
	}

	// Release global handles.
	UnLoadNPL();

	for (int ssrcn = 0; ssrcn < SSRCsCount; ssrcn++)
	{
		//double diff = SSRCs[ssrcn].TimeStampSenderDouble - SSRCs[ssrcn].TimeStampReceiverDouble;
		//if (diff < 0) diff *= -1;
		//if (diff > SSRCs[ssrcn].TimeStampSenderDouble)
		//	SSRCs[ssrcn].isAudio = true;

		SSRCs[ssrcn].rate = SSRCs[ssrcn].TimeStampSenderDouble / SSRCs[ssrcn].TimeStampReceiverDouble;
		if (SSRCs[ssrcn].rate > 80) SSRCs[ssrcn].rateReal = 90;
		else if (SSRCs[ssrcn].rate > 40) SSRCs[ssrcn].rateReal = 48;
		else if (SSRCs[ssrcn].rate > 14) SSRCs[ssrcn].rateReal = 16;
		else SSRCs[ssrcn].rateReal = 8;
		if (SSRCs[ssrcn].rate < 80)
		{
			SSRCs[ssrcn].isAudio = true;
		}
	}


}



int __cdecl wmain(int argc, WCHAR* argv[])
{
	ULONG ret = ERROR_SUCCESS;
	// The first paramryrt should be a file.
	//if (argc <= 3){
	//	wprintf(L"Expect a file name, start frame num, finish frame num\n");
	//	return -1;
	//}

	WCHAR *SSRC = (WCHAR *)malloc(11 * sizeof(WCHAR));
	bool SSRCgiven = false;

	int framerateKHz = 90;
	if (argc > 4)
	{
		if (!_wcsicmp(argv[5], L"AUDIO")) framerateKHz = 16;

		for (int k = 0; k < 10; k++)
		{
			SSRC[k] = argv[4][k];
		}
		SSRC[10] = 0;

		SSRCgiven = true;
	}


	
	if (!SSRCgiven)
	{
		FindAllSSRCs(argv[1]);
	}

	WCHAR filenameBase[1024];
	WCHAR foldername[1024];
	WCHAR filenameSessions[1024];
	WCHAR filename[1024];
	//wcscpy_s(filenameBase, argv[1]);
	//PathRemoveExtension(filenameBase);
	wcscpy_s(filename, argv[1]);
	PathStripPath(filename);
	PathRemoveExtension(filename);

	wcscpy_s(foldername, argv[1]);
	PathRemoveFileSpec(foldername);
	wcscat_s(foldername, 1024, L"\\");
	wcscat_s(foldername, 1024, filename);
	wcscat_s(foldername, 1024, L"\\");

	CreateDirectory(foldername, NULL);

	wcscpy_s(filenameBase, foldername);
	wcscat_s(filenameBase, 1024, filename);
	wcscpy_s(filenameSessions, filenameBase);
	wcscat_s(filenameSessions, 1024, L"_sessions.txt");

	//wcsncpy_s(filenameBase, argv[1], wcslen(argv[1]) - 4);
	FILE *sessionsStream;
	_wfopen_s(&sessionsStream, filenameSessions, L"w+");


	for (int ssrcn = 0; ssrcn < SSRCsCount; ssrcn++)
	{
		fwprintf_s(sessionsStream, L"%s %s %s %s %f %f\n", SSRCs[ssrcn].SSRTId, SSRCs[ssrcn].Source, SSRCs[ssrcn].Destination, SSRCs[ssrcn].isAudio ? L"AUDIO" : L"VIDEO", SSRCs[ssrcn].rate, SSRCs[ssrcn].rateReal);
	}

	fclose(sessionsStream);

	for (int ssrcn = 0; ssrcn < SSRCsCount; ssrcn++)
	{
		wcscpy_s(SSRCs[ssrcn].filename, filenameBase);
		wcscat_s(SSRCs[ssrcn].filename, 1024, L"_");
		wcscat_s(SSRCs[ssrcn].filename, 1024, SSRCs[ssrcn].SSRTId);
		wcscat_s(SSRCs[ssrcn].filename, 1024, L"_(");
		wcscat_s(SSRCs[ssrcn].filename, 1024, SSRCs[ssrcn].Source);
		wcscat_s(SSRCs[ssrcn].filename, 1024, L"-to-");
		wcscat_s(SSRCs[ssrcn].filename, 1024, SSRCs[ssrcn].Destination);
		wcscat_s(SSRCs[ssrcn].filename, 1024, SSRCs[ssrcn].isAudio ? L")_A" : L")_V");
		wcscat_s(SSRCs[ssrcn].filename, 1024, L".txt");
		_wfopen_s(&(SSRCs[ssrcn].sessionsStream), SSRCs[ssrcn].filename, L"w+");

		
		//		fwprintf_s(sessionsStream, L"%s %s %s %s\n", SSRCs[ssrcn].SSRTId, SSRCs[ssrcn].Source, SSRCs[ssrcn].Destination, SSRCs[ssrcn].isAudio ? L"AUDIO" : L"VIDEO");
	}


	//sessionsStream = _wfopen("", "w+");

	// Open the specified capture file.
	HANDLE myCaptureFile = INVALID_HANDLE_VALUE;
	if (ERROR_SUCCESS == NmOpenCaptureFile(argv[1], &myCaptureFile))
	{
		ULONG FrameNumberFrom = _wtol(argv[2]) - 1;
		ULONG FrameNumberTo = _wtol(argv[3]);


		// Initialize the parser engine and return a frame parser.
		HANDLE myFrameParser = MyLoadNPL();
		if (myFrameParser != INVALID_HANDLE_VALUE)
		{
			ULONG myFrameCount = 0;
			ret = NmGetFrameCount(myCaptureFile, &myFrameCount);
			if (ret == ERROR_SUCCESS)
			{
				if (FrameNumberTo <= 0) FrameNumberTo = myFrameCount;
				HANDLE myRawFrame = INVALID_HANDLE_VALUE;
				//for (ULONG fNum = 100; fNum < myFrameCount; fNum++)
				UINT64 TimeStampSource_First = 0;
				UINT64 TimeStampHost_First = 0;

				int SSRCsCount = 0;

				for (ULONG fNum = FrameNumberFrom; fNum < FrameNumberTo; fNum++)
				{
					HANDLE myParsedFrame = INVALID_HANDLE_VALUE;
					ret = NmGetFrame(myCaptureFile, fNum, &myRawFrame);

					if (ret == ERROR_SUCCESS)
					{
						// The last parameter is for the API to return
						// the reassembled frame if enabled.
						// NULL indicates that the API discards the
						// reassembled frame.

						ret = NmParseFrame(myFrameParser, myRawFrame, fNum, NmFieldDisplayStringRequired, &myParsedFrame, NULL);
						if (ret == ERROR_SUCCESS)
						{
							//start field parce

							ULONG myFieldCount;
							ret = NmGetFieldCount(myParsedFrame, &myFieldCount);
							if (ret != ERROR_SUCCESS)
							{
								wprintf(L"Error getting field count, ret = %d\n", ret);
								//return ret;
								continue;
							}
							else
							{
								// Keeps track of a negative offset so protocols start at zero.
								int IndentOffset = 0;

								//wprintf(L"Field count = %d\n", myFieldCount);

								WCHAR *SyncSourceIdStr = (WCHAR *)malloc(11 * sizeof(WCHAR));
								WCHAR *TimestampStr = (WCHAR *)malloc(11 * sizeof(WCHAR));
								WCHAR *SequenceNumberStr = (WCHAR *)malloc(6 * sizeof(WCHAR));
								//WCHAR Source[255];
								//WCHAR Destination[255];
								WCHAR Marker[255];
								int MarkerInt = 0;

								UINT64 TimeStampSource = 0;

								bool foundRTPdata = false;

								// Iterate through all the fields in this frame.
								for (ULONG j = 0; j < myFieldCount; j++)
								{
									NM_PARSED_FIELD_INFO myParsedDataFieldInfo;
									myParsedDataFieldInfo.Size = sizeof(NM_PARSED_FIELD_INFO);

									// Call with Display String Required as the only option.
									ret = NmGetParsedFieldInfo(myParsedFrame,
										j,
										NmFieldDisplayStringRequired,
										&myParsedDataFieldInfo);

									if (ret != ERROR_SUCCESS)
									{
										//wprintf(L"Error getting parsed field #%d info, ret = %d\n", j, ret);
										//return ret;
										continue;
									}

									if (j > 0 && myParsedDataFieldInfo.ValueType == 0)
									{
										IndentOffset--;
										//wprintf(L"\n");
									}

									for (int i = 0; i < (int)myParsedDataFieldInfo.FieldIndent + IndentOffset; i++)
									{
										//wprintf(L"  ");
									}

									// Allocate space for the field name and get it.
									ULONG FieldNameLength = (myParsedDataFieldInfo.NamePathLength + 1) * sizeof(WCHAR);
									WCHAR *FieldName = (WCHAR *)malloc(FieldNameLength);
									ret = NmGetFieldName(myParsedFrame, j, NmFieldNamePath, FieldNameLength, FieldName);
									if (ret != ERROR_SUCCESS)
									{
										wprintf(L"Error %d trying to retreive field name for frame %d, element %d.", ret, fNum, j);
									}

									if (!wcscmp(FieldName, L"SequenceNumber") || 
										!wcscmp(FieldName, L"Timestamp") || 
										!wcscmp(FieldName, L"SyncSourceId") || 
										!wcscmp(FieldName, L"SourceAddress") || 
										!wcscmp(FieldName, L"DestinationAddress") || 
										!wcscmp(FieldName, L"Marker"))
									{
										foundRTPdata = true;
										// Allocate space for the display string and get it.
										ULONG DisplayFormatLength = (myParsedDataFieldInfo.DisplayStringLength + 1) * sizeof(WCHAR);
										WCHAR *DisplayFormat = (WCHAR *)malloc(DisplayFormatLength);
										ret = NmGetFieldName(myParsedFrame, j, NmFieldDisplayString, DisplayFormatLength, DisplayFormat);

										if (ret != ERROR_SUCCESS)
										{
											wprintf(L"Error %d tryin to retreive display name for frame %d element %d.", ret, fNum, j);
										}
										else {
											if (!wcscmp(FieldName, L"SequenceNumber"))
											{
												int k = 0;
												for (k = 0; k < 5; k++)
												{
													SequenceNumberStr[k] = DisplayFormat[k];
													if (DisplayFormat[k + 1] == ' ') break;
												}
												SequenceNumberStr[k + 1] = 0;
												//wprintf(L"%s\n", DisplayFormatCut);
											}
											if (!wcscmp(FieldName, L"Timestamp"))
											{
												int k = 0;
												for (k = 0; k < 10; k++)
												{
													TimestampStr[k] = DisplayFormat[k];
													if (DisplayFormat[k + 1] == ' ') break;
												}
												TimestampStr[k + 1] = 0;

												TimeStampSource = _wtoi64(TimestampStr);
												//wprintf(L"%s ", DisplayFormatCut);
											}

											if (!wcscmp(FieldName, L"Marker"))
											{
												wcscpy_s(Marker, DisplayFormat);
												if (!wcscmp(Marker, L"     (........1.......) Marker set"))
													MarkerInt = 1;
											}


											if (!wcscmp(FieldName, L"SourceAddress"))
											{
												//wcscpy_s(Source, );
												wcscpy_s(SSRCs[SSRCsCount].Source, DisplayFormat);
											}

											if (!wcscmp(FieldName, L"DestinationAddress"))
											{
												wcscpy_s(SSRCs[SSRCsCount].Destination, DisplayFormat);
											}


											if (!wcscmp(FieldName, L"SyncSourceId"))
											{

												int k = 0;
												for (k = 0; k < 10; k++)
												{
													SyncSourceIdStr[k] = DisplayFormat[k];
													if (DisplayFormat[k + 1] == ' ') break;
												}
												SyncSourceIdStr[k + 1] = 0;

												bool found = false;
												for (int jj = 0; jj < SSRCsCount; jj++)
												{
													if (!wcscmp(SSRCs[jj].SSRTId, SyncSourceIdStr))
													{
														found = true;
														break;
													}
												}
												if (!found)
												{
													wcscpy_s(SSRCs[SSRCsCount].SSRTId, SyncSourceIdStr);
													SSRCsCount++;
												}

												//wprintf(L"%s ", DisplayFormatCut);
											}
										}
										free(DisplayFormat);
									}
									free(FieldName);
								}


								UINT64 TimeStamp = 0;
								ret = NmGetFrameTimeStamp(myRawFrame, &TimeStamp);
								//printf("%T{MM/dd/yyyy}, %t{HH':'mm':'ss} .%t{ffff} ", (pTimeStamp * 10000000) + 0x19DB1DED53E8000, (pTimeStamp * 10000000) + 0x19DB1DED53E8000, (pTimeStamp * 10000000) + 0x19DB1DED53E8000);

								UINT64 TimeStampHost = 0;


								//if (foundRTPdata)
								//	if (SSRCgiven)
								//		if (wcscmp(SyncSourceIdStr, SSRC) == 0)
								//		{
								//			if (TimeStampSource_First == 0) TimeStampSource_First = TimeStampSource;
								//			UINT64 TimeStampSource_adj = TimeStampSource - TimeStampSource_First;
								//			if (TimeStampHost_First == 0) TimeStampHost_First = TimeStamp;
								//			TimeStampHost = TimeStamp - TimeStampHost_First;

								//			double TimeStampHostDouble = TimeStampHost / double(10000000);
								//			double TimeStampSourceDouble = TimeStampSource_adj / double(1000) / framerateKHz;
								//			//wprintf(L"%i %s %I64d %I64d %I64d %f %f %s %s\n", fNum + 1, SyncSourceIdStr, TimeStamp, TimeStampHost, TimeStampSource, TimeStampHostDouble, TimeStampSourceDouble, TimestampStr, SequenceNumberStr);
								//			wprintf(L"%i %f %f %lli %lli %s %i\n", fNum + 1, TimeStampHostDouble, TimeStampSourceDouble, TimeStampHost, TimeStampSource, SequenceNumberStr, MarkerInt);
								//		}

								for (int ssrcn = 0; ssrcn < SSRCsCount; ssrcn++)
								{
									if (wcscmp(SyncSourceIdStr, SSRCs[ssrcn].SSRTId) == 0)
									{
										//int framerateKHz = 90;
										//if (SSRCs[ssrcn].isAudio) framerateKHz = 8;

										double framerateKHz = SSRCs[ssrcn].rateReal;
										
										if (SSRCs[ssrcn].TimeStampSenderFirst == 0) SSRCs[ssrcn].TimeStampSenderFirst = TimeStampSource;
										SSRCs[ssrcn].TimeStampSenderLast = TimeStampSource - SSRCs[ssrcn].TimeStampSenderFirst;
										if (SSRCs[ssrcn].TimeStampReceiverFirst == 0) SSRCs[ssrcn].TimeStampReceiverFirst = TimeStamp;
										SSRCs[ssrcn].TimeStampReceiverLast = TimeStamp - SSRCs[ssrcn].TimeStampReceiverFirst;

										SSRCs[ssrcn].TimeStampReceiverDouble = SSRCs[ssrcn].TimeStampReceiverLast / double(10000000);
										SSRCs[ssrcn].TimeStampSenderDouble = SSRCs[ssrcn].TimeStampSenderLast / double(1000) / framerateKHz;

										//wprintf(L"%i %s %I64d %I64d %I64d %f %f %s %s\n", fNum + 1, SyncSourceIdStr, TimeStamp, TimeStampHost, TimeStampSource, TimeStampHostDouble, TimeStampSourceDouble, TimestampStr, SequenceNumberStr);
										fwprintf_s(SSRCs[ssrcn].sessionsStream, L"%i %f %f %lli %lli %s %i\n", fNum + 1, SSRCs[ssrcn].TimeStampReceiverDouble, SSRCs[ssrcn].TimeStampSenderDouble, TimeStampHost, TimeStampSource, SequenceNumberStr, MarkerInt);
									}
								}



							}

							//end field parce


							// Test to see if this frame passed the filter.
							//BOOL passed = FALSE;
							//ret = NmEvaluateFilter(myParsedFrame, myHTTPFilterID, &passed);
							//if ((ret == ERROR_SUCCESS) && (passed == TRUE))
							//{
							// Obtain the value of http.request.uri from
							// the frame. Strings are passed as a word
							// pointer to a Unicode string in the
							// variant.
							//WCHAR value[256];
							//ret = NmGetFieldValueString(myParsedFrame, myHTTPFieldID, 256, (LPWSTR)value);
							//if (ret == ERROR_SUCCESS)
							//{
							//	// Cast to WCHAR *.
							//	wprintf(L"Frame %d: HTTP: %s\n", i + 1, (LPWSTR)value);
							//}
							//}

							// Release the current parsed frame.
							NmCloseHandle(myParsedFrame);
						}
						else
						{
							// Print an error, but continue to loop.
							wprintf(L"Error: 0x%X, trying to parse frame %d\n", ret, fNum + 1);
						}

						// Release the current raw frame.
						NmCloseHandle(myRawFrame);
					}
					else
					{
						// Print an error, but continue to loop.
						wprintf(L"Errors getting raw frame %d\n", fNum + 1);
					}
				}
				for (int i = 0; i < SSRCsCount; i++)
				{
					if (!SSRCgiven) wprintf(L"%s %s %s\n", SSRCs[i].SSRTId, SSRCs[i].Source, SSRCs[i].Destination);
				}
			}

			NmCloseHandle(myFrameParser);

		}
		else
		{
			wprintf(L"Errors creating frame parser\n");
		}

		NmCloseHandle(myCaptureFile);
	}
	else
	{
		wprintf(L"Errors openning capture file\n");
	}

	// Release global handles.
	UnLoadNPL();


	for (int ssrcn = 0; ssrcn < SSRCsCount; ssrcn++)
	{
		fclose(SSRCs[ssrcn].sessionsStream);
		//		fwprintf_s(sessionsStream, L"%s %s %s %s\n", SSRCs[ssrcn].SSRTId, SSRCs[ssrcn].Source, SSRCs[ssrcn].Destination, SSRCs[ssrcn].isAudio ? L"AUDIO" : L"VIDEO");
	}


	return 0;
}
