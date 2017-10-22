function res = GammaCDF(t,a,b)
%returns value of Gamma CDF with parameters a,b
res = gamcdf(t,a,b, 'upper');
end