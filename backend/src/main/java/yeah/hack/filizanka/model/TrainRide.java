package yeah.hack.filizanka.model;


import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.ManyToOne;
import javax.persistence.OrderColumn;
import java.util.List;

@Entity
@AllArgsConstructor
@NoArgsConstructor
@Data
public class TrainRide {

    @Id
    private String trainRideId;

    @ManyToOne
    private Train train;

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(name = "train_ride_point",
            joinColumns = @JoinColumn(name = "train_ride_id"),
            inverseJoinColumns = @JoinColumn(name = "point"))
    @OrderColumn(name="INDEX")
    private List<Point> points;

    @ManyToOne
    private Point lastVisitedPoint;

}
