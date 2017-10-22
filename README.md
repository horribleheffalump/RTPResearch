# RTPResearch
A tool for Real-time Transport Protocol (RTP) video streaming analysis based on hidden Markov model (HMM) approach.
The dynamics of an RTP channel is assumed to be described by an unobservable Markov process with a finite number of states. 
The observation is a non-Markovian multivariate point process that indicates heterogenous frames reception. This tool is
an implementation of the algorithm of the hidden link state estimation given the observable multivariate point process. 
All the details are provided in [1].

## RTPClasses
A class library, which contains the model description and the estimation algorithm implementation. Uses MATLAB through interop.

## RTPMonitor
A simple winforms app for the RTPClasses lib interaction. Accepts [Wireshark](https://www.wireshark.org/docs/man-pages/tshark.html) 
text trace files as an input. One session is necessary for the model
parameters identification. After the parameters of the RTP model are identified, it may be used for channel state estimation
in RTP sessions recorded in other files.

### CAP
CAPTools, CAPTest, CAPParcerStarter: tools for pcap trace files from Microsoft Network Monitor 3.4 parsing. Moved to a more handy
tshark output format. Left as a legacy.


## References

[[1]](https://link.springer.com/chapter/10.1007/978-3-319-23126-6_21) Borisov A., Bosov A., Miller G. 
Modeling and Monitoring of RTP Link on the Receiver Side // 
In Proceedings of Next-Generation Wired/Wireless Advanced Networks and Systems Conference, 2015, St. Petersburg, Russia,
Springer Lecture Notes in Computer Science No. 9247, pp. 229-241. DOI: 10.1007/978-3-319-23126-6_21. 
[Researchgate](https://www.researchgate.net/publication/281275605_Modeling_and_Monitoring_of_RTP_Link_on_the_Receiver_Side)
