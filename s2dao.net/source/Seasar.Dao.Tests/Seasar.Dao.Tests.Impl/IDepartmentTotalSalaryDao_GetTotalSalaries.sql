SELECT d.deptno, sum(e.sal) as totalSalary FROM dept d, emp e WHERE d.deptno = e.deptno GROUP BY d.deptno